using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeService_Api.Notifications;
using ShoeService_Data.Infrastructure;
using ShoeService_Data.IRepository;
using ShoeService_Data;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using ShoeService_Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Api.Authorization;
using ShoeService_Common.Constants;
using ShoeService_Data.Repository;

namespace ShoeService_Api.Controllers
{
    [Authorize(Roles = $"{RoleManager.RoleAdmin}, {RoleManager.RoleStaff}, {RoleManager.RoleStoreManager}")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //[ClaimRequirement(FunctionCode.CATEGORY, CommandCode.VIEW)]
        //public async Task<ActionResult> Get()
        //{
        //    ResponseResult responseResult = new ResponseResult();

        //    var category = await _categoryRepository.GetAll().ToListAsync();
        //    var categoryViewModels = new List<CategoryViewModel>();
        //    if (category != null)
        //    {
        //        responseResult.StatusCode = (int)HttpStatusCode.OK;
        //        responseResult.Status = CustomerNotification.Success;
        //        responseResult.Message = CustomerNotification.Get_Success;
        //        responseResult.Data = category;

        //        return Ok(responseResult);
        //    }
        //    else
        //    {
        //        responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
        //        responseResult.Status = CustomerNotification.Fail;
        //        responseResult.Message = CustomerNotification.Get_Fail;

        //        return BadRequest(responseResult);
        //    }

        //}

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.CATEGORY, CommandCode.VIEW)]
        public async Task<IActionResult> GetPagingCategory(string? filter, int? pageIndex, int? pageSize)
        {
            var query = await _categoryRepository.GetAll().ToListAsync();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(r => r.Name.Contains(filter)).ToList();
                }
                var totalRecord = query.Count();

                pageSize = pageSize == null ? 25 : pageSize.Value;
                pageIndex = pageIndex == null ? 1 : pageIndex.Value;

                var item = query
                    .Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

                var pagination = new Pagination<CategoryDto>()
                {
                    Items = _mapper.Map<List<CategoryDto>>(item),
                    TotalRecords = totalRecord,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.OK,
                    Status = NotificationBase.Success
                };

                return Ok(pagination);
            }
            else
            {
                var pagination = new Pagination<CategoryDto>()
                {
                    TotalRecords = 0,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return BadRequest(pagination);
            }
        }

        [HttpGet("{id}")]
        [ClaimRequirement(FunctionCode.CATEGORY, CommandCode.VIEW)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            await Task.Yield();
            var category = _categoryRepository.GetSingleById(id);

            if (category == null)
            {
                var pagination = new Pagination<CategoryDto>()
                {
                    TotalRecords = 0,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return BadRequest(pagination);
            }
            else
            {
                var pagination = new Pagination<CategoryDto>()
                {
                    Items = _mapper.Map<CategoryDto>(category),
                    TotalRecords = 1,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return Ok(pagination);
            }
        }

        [HttpPost("Create")]
        [ClaimRequirement(FunctionCode.CATEGORY, CommandCode.CREATE)]
        public async Task<ActionResult> Create(CategoryDto categoryDto)
        {
            ResponseResult responseResult = new ResponseResult();

            if (categoryDto != null)
            {
                var category = _mapper.Map<Category>(categoryDto);
                _categoryRepository.Add(category);
                if (await _unitOfWork.CommitAsync() >= 0)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.OK;
                    responseResult.Status = CustomerNotification.Success;
                    responseResult.Message = CustomerNotification.Create_Success;
                    responseResult.Data = category;

                    return Ok(responseResult);
                }
                else
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = CustomerNotification.Fail;
                    responseResult.Message = CustomerNotification.Create_Fail;

                    return BadRequest(responseResult);
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = CustomerNotification.Fail;
                responseResult.Message = CustomerNotification.Create_Fail;

                return BadRequest(responseResult);
            }
        }

        [HttpPut("Update")]
        [ClaimRequirement(FunctionCode.CATEGORY, CommandCode.UPDATE)]
        public async Task<ActionResult> Update(CategoryDto categoryDto, [FromServices] ShoeServiceDbContext context)
        {
            ResponseResult responseResult = new ResponseResult();
            if (categoryDto != null)
            {
                var existShoes = context.Customers.FirstOrDefault(x => x.Id == categoryDto.Id);
                if (existShoes == null)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = ShoeNotification.Fail;
                    responseResult.Message = ShoeNotification.Get_Fail;

                    return BadRequest(responseResult);
                }
                else
                {
                    var category = _mapper.Map<Category>(categoryDto);
                    _categoryRepository.Add(category);
                    if (await _unitOfWork.CommitAsync() >= 0)
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.OK;
                        responseResult.Status = ShoeNotification.Success;
                        responseResult.Message = ShoeNotification.Update_Success;
                        responseResult.Data = categoryDto;

                        return Ok(responseResult);
                    }
                    else
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseResult.Status = ShoeNotification.Fail;
                        responseResult.Message = ShoeNotification.Update_Fail;

                        return BadRequest(responseResult);
                    }
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = ShoeNotification.Fail;
                responseResult.Message = ShoeNotification.Create_Fail;

                return BadRequest(responseResult);
            }
        }

        [HttpDelete("Delete")]
        [ClaimRequirement(FunctionCode.CATEGORY, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != 0)
            {
                var shoes = _categoryRepository.GetSingleById(id);
                if (shoes != null)
                {

                    if (await _unitOfWork.CommitAsync() >= 0)
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.OK;
                        responseResult.Status = ShoeNotification.Success;
                        responseResult.Message = ShoeNotification.Delete_Success;
                        responseResult.Data = shoes;

                        return Ok(responseResult);
                    }
                    else
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseResult.Status = ShoeNotification.Fail;
                        responseResult.Message = ShoeNotification.Create_Fail;

                        return BadRequest(responseResult);
                    }
                }
                else
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = ShoeNotification.Fail;
                    responseResult.Message = ShoeNotification.Get_Fail;

                    return BadRequest(responseResult);
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = ShoeNotification.Fail;
                responseResult.Message = ShoeNotification.Delete_Fail;

                return BadRequest(responseResult);
            }
        }
    }
}
