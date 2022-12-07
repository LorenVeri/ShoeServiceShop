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

namespace ShoeService_Api.Controllers
{
    [Authorize(Roles = $"{RoleManager.RoleAdmin}, {RoleManager.RoleStaff}, {RoleManager.RoleStoreManager}, {RoleManager.RoleMember}")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("filter")]
        [ClaimRequirement(FunctionCode.CUSTOMER, CommandCode.VIEW)]
        public async Task<IActionResult> GetPaging(string? filter, int? pageIndex, int? pageSize)
        {
            var query = await _customerRepository.GetAll().ToListAsync();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    query = query.Where(r => r.FirstName.Contains(filter) || r.LastName.Contains(filter) || r.CustomerEmail.Contains(filter)).ToList();
                }
                var totalRecord = query.Count();

                pageSize = pageSize == null ? 25 : pageSize.Value;
                pageIndex = pageIndex == null ? 1 : pageIndex.Value;

                var item = query
                    .Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();

                var pagination = new Pagination<CustomerDto>()
                {
                    Items = _mapper.Map<List<CustomerDto>>(item),
                    TotalRecords = totalRecord,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.OK,
                    Status = NotificationBase.Success
                };

                return Ok(pagination);
            }
            else
            {
                var pagination = new Pagination<CustomerDto>()
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
        [ClaimRequirement(FunctionCode.CUSTOMER, CommandCode.VIEW)]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Yield();
            var customer = _customerRepository.GetSingleById(id);

            if (customer == null)
            {
                var pagination = new Pagination<CustomerDto>()
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
                var pagination = new Pagination<CustomerDto>()
                {
                    Items = _mapper.Map<CustomerDto>(customer),
                    TotalRecords = 1,
                    Message = NotificationBase.Get_Success,
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = NotificationBase.Fail
                };

                return Ok(pagination);
            }
        }

        [HttpPost("Create")]
        [ClaimRequirement(FunctionCode.CUSTOMER, CommandCode.CREATE)]
        public async Task<ActionResult> Create(CustomerDto customerDto)
        {
            ResponseResult responseResult = new ResponseResult();

            if (customerDto != null)
            {
                var customer = _mapper.Map<Customer>(customerDto);
                _customerRepository.Add(customer);
                if (await _unitOfWork.CommitAsync() >= 0)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.OK;
                    responseResult.Status = CustomerNotification.Success;
                    responseResult.Message = CustomerNotification.Create_Success;
                    responseResult.Data = customerDto;

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
        [ClaimRequirement(FunctionCode.CUSTOMER, CommandCode.UPDATE)]
        public async Task<ActionResult> Update(CustomerDto customerDto, [FromServices] ShoeServiceDbContext context)
        {
            ResponseResult responseResult = new ResponseResult();
            if (customerDto != null)
            {
                var existShoes = context.Customers.FirstOrDefault(x => x.Id == customerDto.Id);
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
                    var customer = _mapper.Map<Customer>(customerDto);
                    _customerRepository.Add(customer);
                    if (await _unitOfWork.CommitAsync() >= 0)
                    {
                        responseResult.StatusCode = (int)HttpStatusCode.OK;
                        responseResult.Status = ShoeNotification.Success;
                        responseResult.Message = ShoeNotification.Update_Success;
                        responseResult.Data = customerDto;

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
        [ClaimRequirement(FunctionCode.CUSTOMER, CommandCode.DELETE)]
        public async Task<ActionResult> Delete(int id)
        {
            ResponseResult responseResult = new ResponseResult();

            if (id != 0)
            {
                var shoes = _customerRepository.GetSingleById(id);
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
