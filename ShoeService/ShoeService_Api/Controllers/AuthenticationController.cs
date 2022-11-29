using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShoeService_Data;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ShoeService_Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model, [FromServices] ShoeServiceDbContext dbContext)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result)
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = await GetClaim(user, dbContext);

                    authClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            else if (user != null && user.AccessFailedCount <= 5)
            {
                user.AccessFailedCount++;
                await dbContext.SaveChangesAsync();
                if (user.AccessFailedCount > 5)
                {
                    user.LockoutEnabled = true;
                    return BadRequest(new
                    {
                        Title = "Tài khoản của bạn bị khóa",
                        Message = "Bạn đã đăng nhập sai mật khẩu quá 5 lần",
                    });
                }
                else
                    return BadRequest(new
                    {
                        Title = "Đăng nhập thất bại",
                        Message = "Bạn nhập sai mật khẩu",
                    });
            }

            return BadRequest(new
            {
                Title = "Đăng nhập thất bại",
                Message = "Bạn nhập sai email hoặc mật khẩu",
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, [FromServices] ShoeServiceDbContext _context)
        {
            ResponseResult response = new ResponseResult();
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user == null)
            {
                DateTime date = DateTime.Now;
                user = _mapper.Map<User>(registerDto);
                user.CreatedDate = date;
                user.Dob = date;
                user.UserName = ConvertToUnsign((user.FirstName + " " + user.LastName).Trim().ToLower().Replace(" ", ""));

                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded && user.IsActive == true)
                {
                    response.Status = "Thành công";
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Đăng ký tài khoản thành công";
                    response.Data = result;
                    return Ok(response);
                }
                else
                {
                    if (user.AccessFailedCount <= 5)
                        user.AccessFailedCount++;
                    else
                        user.LockoutEnabled = false;
                    await _context.SaveChangesAsync();

                    string notification = "";
                    foreach (var error in result.Errors)
                    {
                        notification += error.Description;
                    }

                    response.Status = "Thất bại";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = notification;
                    response.Data = registerDto;
                    return BadRequest(response);
                }
            }
            else
            {
                return BadRequest("Tài khoản đã tồn tại");
            }
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private async Task<List<Claim>> GetClaim(User user, [FromServices] ShoeServiceDbContext _context)
        {
            var roles = await _userManager.GetRolesAsync(user);

            //var query = from p in _context.Permissions
            //            join c in _context.Commands
            //              on p.CommandId equals c.Id
            //            join f in _context.Functions
            //              on p.FunctionId equals f.Id
            //            join r in _roleManager.Roles
            //              on p.RoleId equals r.Id
            //            where roles.Contains(r.Name)
            //            select f.Id + "_" + c.Id;

            //var permissions = await query.Distinct().ToListAsync();

            var identity = new List<Claim>();
            identity.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
            identity.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""));
            identity.Add(new Claim("FullName", (user.FirstName + " " + user.LastName) ?? ""));
            identity.Add(new Claim("UserID", user.Id ?? ""));
            identity.Add(new Claim(ClaimTypes.Role, string.Join(";", roles)));
            //identity.Add(new Claim(SystemConstants.Claims.Permissions, JsonConvert.SerializeObject(permissions)));

            return identity;
        }

        private static string ConvertToUnsign(string strInput)
        {
            Regex ConvertToUnsign_rg = null;
            if (ReferenceEquals(ConvertToUnsign_rg, null))
            {
                ConvertToUnsign_rg = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            }
            var temp = strInput.Normalize(NormalizationForm.FormD);
            return ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D").ToLower();
        }

    }
}
