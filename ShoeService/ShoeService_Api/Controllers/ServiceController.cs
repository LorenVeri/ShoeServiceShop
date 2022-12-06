﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Api.Notifications;
using ShoeService_Common.Constants;
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
    public class ServiceController : BaseController
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger<ServiceController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ServiceController(IServiceRepository serviceRepository, ILogger<ServiceController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.VIEW)]
        public async Task<ActionResult> Get()
        {
            ResponseResult responseResult = new ResponseResult();

            var service = await _serviceRepository.GetAll().ToListAsync();
            if (service != null)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Get_Success;
                responseResult.Data = service;

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

        [HttpPost]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.CREATE)]
        public async Task<IActionResult> Post(ServiceDto entity)
        {

            ResponseResult responseResult = new ResponseResult();
            if (!ModelState.IsValid)
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = CustomerNotification.Fail;
                responseResult.Message = CustomerNotification.Create_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }

            var service = _mapper.Map<Service>(entity);
            _serviceRepository.Add(service);
            if(await _unitOfWork.CommitAsync() > 0)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Create_Success;
                responseResult.Data = service;

                return BadRequest(responseResult);
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

        [HttpPut]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.UPDATE)]
        public async Task<IActionResult> Put(ServiceDto entity)
        {

            ResponseResult responseResult = new ResponseResult();
            if (!ModelState.IsValid)
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = CustomerNotification.Fail;
                responseResult.Message = CustomerNotification.Update_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }

            var service = _mapper.Map<Service>(entity);
            _serviceRepository.Update(service);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Update_Success;
                responseResult.Data = service;

                return BadRequest(responseResult);
            }
            else
            {
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Status = CustomerNotification.Fail;
                responseResult.Message = CustomerNotification.Update_Fail;
                responseResult.Data = null;

                return BadRequest(responseResult);
            }

        }

        [HttpDelete("Delete")]
        [ClaimRequirement(FunctionCode.SERVICES, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != null || id > 0)
            {
                var shoes = _serviceRepository.GetSingleById(id);
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
