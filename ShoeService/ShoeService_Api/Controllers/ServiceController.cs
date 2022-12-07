using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Api.Notifications;
using ShoeService_Common.Constants;
using ShoeService_Common.Extention;
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

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.MEMBERSHIP, CommandCode.VIEW)]
        public async Task<IActionResult> GetPaging(string? filter, int? pageIndex, int? pageSize)
        {
            var query = await _serviceRepository.GetAll().ToListAsync();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(r => r.Name.Contains(filter.ConvertToUnSign()) || r.Code.Contains(filter)).ToList();
                }
                var totalRecord = query.Count();

                pageSize = pageSize == null ? 25 : pageSize.Value;
                pageIndex = pageIndex == null ? 1 : pageIndex.Value;

                var item = query
                    .Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

                var pagination = new Pagination<ServiceDto>()
                {
                    Items = _mapper.Map<List<ServiceDto>>(item),
                    TotalRecords = totalRecord,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.OK,
                    Status = NotificationBase.Success
                };

                return Ok(pagination);
            }
            else
            {
                var pagination = new Pagination<ServiceDto>()
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
            var service = _serviceRepository.GetSingleById(id);

            if (service == null)
            {
                var pagination = new Pagination<ServiceDto>()
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
                var pagination = new Pagination<ServiceDto>()
                {
                    Items = _mapper.Map<ServiceDto>(service),
                    TotalRecords = 1,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return Ok(pagination);
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

                return Ok(responseResult);
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

                return Ok(responseResult);
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

            if (id != 0 || id > 0)
            {
                var shoes = _serviceRepository.GetSingleById(id);
                if (shoes != null)
                {

                    if (await _unitOfWork.CommitAsync() > 0)
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
