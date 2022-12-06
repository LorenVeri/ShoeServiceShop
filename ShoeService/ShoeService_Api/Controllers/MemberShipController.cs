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

        [HttpGet]
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.VIEW)]
        public async Task<ActionResult> Get()
        {
            ResponseResult responseResult = new ResponseResult();

            var memberShip = await _memerShipRepository.GetAll().ToListAsync();
            if (memberShip != null)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Get_Success;
                responseResult.Data = memberShip;

                return Ok(responseResult);
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = CustomerNotification.Fail;
                responseResult.Message = CustomerNotification.Get_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }
        }

        [HttpPost("Create")]
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.CREATE)]
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
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.UPDATE)]
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
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != null)
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
