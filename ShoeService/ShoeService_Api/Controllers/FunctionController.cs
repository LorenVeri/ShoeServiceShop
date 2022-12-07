using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Common.Constants;
using ShoeService_Data;
using ShoeService_Data.IRepository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using ShoeService_Model.ViewModel;

namespace ShoeService_Api.Controllers
{
    [Authorize(Roles = $"{RoleManager.RoleAdmin}")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFunctionRepository _functionRepository;

        private readonly ShoeServiceDbContext _context;

        public FunctionController(IMapper mapper, ShoeServiceDbContext context, IFunctionRepository functionRepository)
        {
            _mapper = mapper;
            _context = context;
            _functionRepository = functionRepository;
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetPagingRoles(string? filter, int? pageIndex, int? pageSize)
        {
            var query = _functionRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                var x = query.Where(r => r.Id.Contains(filter));
            }
            var totalRecord = query.Count();

            pageSize = pageSize == null ? 25 : pageSize.Value;
            pageIndex = pageIndex == null ? 1 : pageIndex.Value;

            var item = await query
                .Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToListAsync();

            var pagination = new Pagination<FunctionDto>()
            {
                Items = _mapper.Map<List<FunctionDto>>(item),
                TotalRecords = totalRecord
            };

            return Ok(pagination);
        }

        //URL: GET: http://localhost:5000/api/roles/{id}
        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Yield();
            var functions = _functionRepository.GetSingleById(id);
            if (functions == null) return NotFound();
            
            return Ok(_mapper.Map<FunctionDto>(functions));
        }

        //URL: GET: http://localhost:5000/api/roles/{id}
        [HttpGet("roleId")]
        [ClaimRequirement(FunctionCode.SYSTEM_FUNCTION, CommandCode.VIEW)]
        public async Task<IActionResult> GetByRoleId(string roleId)
        {
            var functions = await _functionRepository.GetFunctionByRole(roleId).Distinct().ToListAsync();
            if (functions.Count() == 0) return NotFound();

            var funcs = new List<FunctionDto>();

            //foreach (var item in functions)
            //{
            //    var childs = new List<FuncChild>();

            //    if (!String.IsNullOrEmpty(item.ParentId))
            //    {
            //        var child = new FuncChild
            //        {
            //            Id = item.Id,
            //            ParentId = item.ParentId,
            //            Icon = item.Icon,
            //            Name = item.Name,
            //            SortOrder = item.SortOrder,
            //            Url = item.Url
            //        };

            //        func.FuncChild.Add(child);
            //    }
            //    else
            //    {
            //        func.Ad
            //    }
            //    funcs.Add(func);
            //}

            return Ok(functions);
        }
    }
}
