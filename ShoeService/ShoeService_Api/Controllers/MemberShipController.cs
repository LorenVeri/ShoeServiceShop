using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Api.Notifications;
using ShoeService_Common.Constants;
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
    public class MemberShipController : BaseController
    {
        private readonly IMemberShipRepository _memerShipRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MemberShipController(IMemberShipRepository memerShipRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _memerShipRepository = memerShipRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.VIEW)]
        public async Task<IActionResult> GetPaging(string? filter, int? pageIndex, int? pageSize)
        {
            var query = await _memerShipRepository.GetAll().ToListAsync();

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

                var pagination = new Pagination<MemberShipDto>()
                {
                    Items = _mapper.Map<List<MemberShipDto>>(item),
                    TotalRecords = totalRecord,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.OK,
                    Status = NotificationBase.Success
                };

                return Ok(pagination);
            }
            else
            {
                var pagination = new Pagination<MemberShipDto>()
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
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Yield();
            var membership = _memerShipRepository.GetSingleById(id);

            if (membership == null)
            {
                var pagination = new Pagination<MemberShipDto>()
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
                var pagination = new Pagination<MemberShipDto>()
                {
                    Items = _mapper.Map<MemberShipDto>(membership),
                    TotalRecords = 1,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return Ok(pagination);
            }
        }

        [HttpPost("Create")]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.CREATE)]
        public async Task<ActionResult> Create(MemberShipDto memberShipDto)
        {
            ResponseResult responseResult = new ResponseResult();

            if (memberShipDto != null)
            {
                var memberShip = _mapper.Map<MemberShip>(memberShipDto);
                _memerShipRepository.Add(memberShip);
                if (await _unitOfWork.CommitAsync() >= 0)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.OK;
                    responseResult.Status = MemberShipNotification.Success;
                    responseResult.Message = MemberShipNotification.Create_Success;
                    responseResult.Data = memberShipDto;

                    return Ok(responseResult);
                }
                else
                {
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Status = MemberShipNotification.Fail;
                    responseResult.Message = MemberShipNotification.Create_Fail;
                    responseResult.Data = null;

                    return BadRequest(responseResult);
                }
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = CustomerNotification.Fail;
                responseResult.Message = CustomerNotification.Create_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }
        }

        [HttpPut("Update")]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.UPDATE)]
        public async Task<ActionResult> Update(MemberShipDto memberShipDto, [FromServices] ShoeServiceDbContext context)
        {
            ResponseResult responseResult = new ResponseResult();
            if (memberShipDto != null)
            {
                var existShoes = context.MemberShips.FirstOrDefault(x => x.Id == memberShipDto.Id);
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
                    var memberShip = _mapper.Map<MemberShip>(memberShipDto);
                    _memerShipRepository.Add(memberShip);
                    if (await _unitOfWork.CommitAsync() >= 0)
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.OK;
                        responseResult.Status = ShoeNotification.Success;
                        responseResult.Message = ShoeNotification.Update_Success;
                        responseResult.Data = memberShipDto;

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
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != 0)
            {
                var shoes = _memerShipRepository.GetSingleById(id);
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
