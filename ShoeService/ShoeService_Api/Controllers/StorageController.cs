using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeService_Api.Authorization;
using ShoeService_Api.Constants;
using ShoeService_Api.Controllers;
using ShoeService_Api.Notifications;
using ShoeService_Common.Constants;
using ShoeService_Data.Infrastructure;
using ShoeService_Data.IRepository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using ShoeService_Model.ViewModel;
using System.Net;

namespace ShoeStorage_Api.Controllers
{
    [Authorize(Roles = $"{RoleManager.RoleAdmin}, {RoleManager.RoleStaff}, {RoleManager.RoleStoreManager}, {RoleManager.RoleMember}")]
    public class StorageController : BaseController
    {
        private readonly IStorageRepository _storageRepository;
        private readonly ILogger<StorageController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StorageController(IStorageRepository storageRepository, ILogger<StorageController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _storageRepository = storageRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.STORAGE, CommandCode.VIEW)]
        public async Task<IActionResult> GetPagingStorage(string? filter, int? pageIndex, int? pageSize)
        {
            var query = await _storageRepository.GetAll().ToListAsync();

            if(query != null)
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

                var pagination = new Pagination<StorageDto>()
                {
                    Items = _mapper.Map<List<StorageDto>>(item),
                    TotalRecords = totalRecord,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.OK,
                    Status = NotificationBase.Success
                };

                return Ok(pagination);
            }else
            {
                var pagination = new Pagination<StorageDto>()
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
        [ClaimRequirement(FunctionCode.STORAGE, CommandCode.VIEW)]
        public async Task<IActionResult> GetStorageById(int id)
        {
            await Task.Yield();
            var storage = _storageRepository.GetSingleById(id);

            if (storage == null)
            {
                var pagination = new Pagination<StorageDto>()
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
                var pagination = new Pagination<StorageDto>()
                {
                    Items = _mapper.Map<StorageDto>(storage),
                    TotalRecords = 1,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return Ok(pagination);
            }
        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.STORAGE, CommandCode.CREATE)]
        public async Task<IActionResult> Post(StorageDto entity)
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

            var storage = _mapper.Map<Storage>(entity);
            _storageRepository.Add(storage);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Create_Success;
                responseResult.Data = storage;

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
        [ClaimRequirement(FunctionCode.STORAGE, CommandCode.UPDATE)]
        public async Task<IActionResult> Put(StorageDto entity)
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

            var storage = _mapper.Map<Storage>(entity);
            _storageRepository.Update(storage);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                responseResult.StatusCode = (int)HttpStatusCode.OK;
                responseResult.Status = CustomerNotification.Success;
                responseResult.Message = CustomerNotification.Update_Success;
                responseResult.Data = storage;

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
        [ClaimRequirement(FunctionCode.STORAGE, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != 0 || id > 0)
            {
                var shoes = _storageRepository.GetSingleById(id);
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
