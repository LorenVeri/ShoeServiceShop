using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeService_Api.Controllers;
using ShoeService_Api.Notifications;
using ShoeService_Data.Infrastructure;
using ShoeService_Data.IRepository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System.Net;

namespace ShoeStorage_Api.Controllers
{
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

        [HttpPost]
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
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != null || id > 0)
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
