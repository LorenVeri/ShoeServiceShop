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

        [HttpGet]
        [ClaimRequirement(FunctionCode.SHOES, CommandCode.VIEW)]
        public async Task<ActionResult> Get()
        {
            ResponseResult responseResult = new ResponseResult();

            var shoes = await _shoeRepository.GetAll().ToListAsync();
            if (shoes != null)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Get_Success;
                responseResult.Data = shoes;

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

            if (id != null)
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
