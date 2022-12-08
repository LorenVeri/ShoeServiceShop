using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Common.Constants;
using ShoeService_Data;
using ShoeService_Model.Models;
using ShoeService_Model.ViewModel;

namespace ShoeService_Api.Controllers
{
    [Authorize(Roles = $"{RoleManager.RoleAdmin}")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShoeServiceDbContext _context;
        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<IdentityRole> roleManager, ILogger<RoleController> logger, ShoeServiceDbContext context)
        {
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetPagingRoles(string? filter, int? pageIndex, int? pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(r => r.Id.Contains(filter) || r.Name.Contains(filter));
            }
            var totalRecord = query.Count();

            pageSize = pageSize == null ? 25 : pageSize.Value;
            pageIndex = pageIndex == null ? 1 : pageIndex.Value;

            var item = await query
                .Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(r => new RoleVM()
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToListAsync();

            var pagination = new Pagination<RoleVM>()
            {
                Items = item,
                TotalRecords = totalRecord
            };

            return Ok(pagination);
        }

        //URL: GET: http://localhost:5000/api/roles/{id}
        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var roleVm = new RoleVM()
            {
                Id = role.Id,
                Name = role.Name
            };
            return Ok(roleVm);
        }

        //URL: POST: http://localhost:5000/api/roles
        [HttpPost]
        public async Task<IActionResult> PostRole(RoleVM roleVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole()
            {
                Id = roleVM.Id,
                Name = roleVM.Name,
                NormalizedName = roleVM.Name.ToUpper()
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, roleVM);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //URL: PUT: http://localhost:5000/api/roles/{id}
        [HttpPut("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.UPDATE)]
        public async Task<IActionResult> PutRole(string id, [FromBody] RoleVM roleVm)
        {
            if (id != roleVm.Id)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            role.Name = roleVm.Name;
            role.NormalizedName = roleVm.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_ROLE, CommandCode.DELETE)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                var roleVM = new RoleVM()
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return Ok(roleVM);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.VIEW)]
        public async Task<IActionResult> GetPermissionByRoleId(string roleId)
        {
            var permissions = from p in _context.Permissions
                              join a in _context.Commands
                              on p.CommandId equals a.Id
                              where p.RoleId == roleId
                              select p;
            if (permissions != null)
            {
                return Ok(await permissions.ToListAsync());
            }
            return BadRequest();
        }

        [HttpPut("{roleId}/permissions")]
        [ClaimRequirement(FunctionCode.SYSTEM_PERMISSION, CommandCode.UPDATE)]
        public async Task<IActionResult> PutPermissionByRoleId(string roleId, [FromBody] List<PermissionVm> request)
        {
            //create new permission list from user changed
            var newPermissions = new List<Permission>();
            foreach (var p in request)
            {
                newPermissions.Add(new Permission(p.FunctionId, roleId, p.CommandId));
            }
            var existingPermissions = _context.Permissions.Where(x => x.RoleId == roleId);

            _context.Permissions.RemoveRange(existingPermissions);
            _context.Permissions.AddRange(newPermissions.Distinct(new MyPermissionComparer()));
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return NoContent();
            }

            return BadRequest();
        }

    }

    internal class MyPermissionComparer : IEqualityComparer<Permission>
    {
        // Items are equal if their ids are equal.
        public bool Equals(Permission x, Permission y)
        {
            // Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y))
                return true;

            // Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the items properties are equal.
            return x.CommandId == y.CommandId && x.FunctionId == x.FunctionId && x.RoleId == x.RoleId;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Permission permission)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(permission, null)) return 0;

            //Get hash code for the ID field.
            int hashProductId = (permission.CommandId + permission.FunctionId + permission.RoleId).GetHashCode();

            return hashProductId;
        }
    }
}
