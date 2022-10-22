using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeService_Data;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;

namespace ShoeService_Api.Controllers
{
    public class AuthenticationController : BaseController
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel, [FromServices] ShoeServiceDbContext _context)
        {
            if(!ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded && user.IsActive == true)
                    {
                        return Ok();
                    }
                    else
                    {
                        if (user.AccessFailedCount <= 5)
                            user.AccessFailedCount++;
                        else
                            user.LockoutEnabled = false;
                        await _context.SaveChangesAsync();
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest("Tài khoản không tồn tại");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPost("register")]    
        public async Task<IActionResult> Register()
    
    }
}
