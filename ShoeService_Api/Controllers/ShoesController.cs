using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Api.Notifications;
using ShoeService_Common.Constants;
using ShoeService_Common.Extention;
using ShoeService_Data;
using ShoeService_Data.Infrastructure;
using ShoeService_Data.IRepository;
using ShoeService_Data.Repository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using ShoeService_Model.ViewModel;
using System.Net;
using static ShoeService_Common.Constants.SystemConstants;

namespace ShoeService_Api.Controllers
{
    [Authorize(Roles = $"{RoleManager.RoleAdmin}, {RoleManager.RoleStaff}, {RoleManager.RoleStoreManager}, {RoleManager.RoleMember}")]
    public class ShoesController : BaseController
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ShoesController(IShoeRepository shoeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _shoeRepository = shoeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.VIEW)]
        public async Task<IActionResult> GetPaging(string? filter, int? pageIndex, int? pageSize)
        {
            var query = await _shoeRepository.GetAll().ToListAsync();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(r => r.Name.Contains(filter.ConvertToUnSign())).ToList();
                }
                var totalRecord = query.Count();

                pageSize = pageSize == null ? 25 : pageSize.Value;
                pageIndex = pageIndex == null ? 1 : pageIndex.Value;

                var item = query
                    .Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

                var pagination = new Pagination<ShoesDto>()
                {
                    Items = _mapper.Map<List<ShoesDto>>(item),
                    TotalRecords = totalRecord,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.OK,
                    Status = NotificationBase.Success
                };

                return Ok(pagination);
            }
            else
            {
                var pagination = new Pagination<ShoesDto>()
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
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Yield();
            var shoes = _shoeRepository.GetSingleById(id);

            if (shoes == null)
            {
                var pagination = new Pagination<ShoesDto>()
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
                var pagination = new Pagination<ShoesDto>()
                {
                    Items = _mapper.Map<ShoesDto>(shoes),
                    TotalRecords = 1,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return Ok(pagination);
            }
        }

        [HttpPost("Create")]
        [ClaimRequirement(FunctionCode.SHOES, CommandCode.CREATE)]
        public async Task<ActionResult> Create(ShoesDto shoeDto)
        {
            ResponseResult responseResult = new ResponseResult();

            if (shoeDto != null)
            {
                var shoe = _mapper.Map<Shoes>(shoeDto);
                _shoeRepository.Add(shoe);
                if (await _unitOfWork.CommitAsync() >= 0)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.OK;
                    responseResult.Status = ShoeNotification.Success;
                    responseResult.Message = ShoeNotification.Create_Success;
                    responseResult.Data = shoeDto;

                    return Ok(responseResult);
                }
                else
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = ShoeNotification.Fail;
                    responseResult.Message = ShoeNotification.Create_Fail;
                    responseResult.Data = null;

                    return BadRequest(responseResult);
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = ShoeNotification.Fail;
                responseResult.Message = ShoeNotification.Create_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }
        }

        [HttpPut("Update")]
        [ClaimRequirement(FunctionCode.SHOES, CommandCode.UPDATE)]
        public async Task<ActionResult> Update(ShoesDto shoeDto, [FromServices] ShoeServiceDbContext context)
        {
            ResponseResult responseResult = new ResponseResult();
            if (shoeDto != null)
            {
                var existShoes = context.Shoes.FirstOrDefault(x => x.Id == shoeDto.Id);
                if (existShoes == null)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = ShoeNotification.Fail;
                    responseResult.Message = ShoeNotification.Get_Fail;
                    responseResult.Data = null;
                    return BadRequest(responseResult);
                }
                else
                {
                    var shoe = _mapper.Map<Shoes>(shoeDto);
                    _shoeRepository.Add(shoe);
                    if (await _unitOfWork.CommitAsync() >= 0)
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.OK;
                        responseResult.Status = ShoeNotification.Success;
                        responseResult.Message = ShoeNotification.Update_Success;
                        responseResult.Data = shoeDto;

                        return Ok(responseResult);
                    }
                    else
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseResult.Status = ShoeNotification.Fail;
                        responseResult.Message = ShoeNotification.Update_Fail;
                        responseResult.Data = null;

                        return BadRequest(responseResult);
                    }
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = ShoeNotification.Fail;
                responseResult.Message = ShoeNotification.Create_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }
        }

        [HttpDelete("Delete")]
        [ClaimRequirement(FunctionCode.SHOES, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != 0)
            {
                var shoes = _shoeRepository.GetSingleById(id);
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
                        responseResult.Data = null;

                        return BadRequest(responseResult);
                    }
                }
                else
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = ShoeNotification.Fail;
                    responseResult.Message = ShoeNotification.Get_Fail;
                    responseResult.Data = null;
                    return BadRequest(responseResult);
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = ShoeNotification.Fail;
                responseResult.Message = ShoeNotification.Delete_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }
        }
    }
}
