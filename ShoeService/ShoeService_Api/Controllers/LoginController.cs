using Microsoft.AspNetCore.Mvc;
using ShoeService_Api.Notifications;
using ShoeService_Data.IRepository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System.Net;

namespace ShoeService_Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public LoginController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("login")]
        public ActionResult CustomerLogin(LoginDto loginDto)
        {
            ResponseResult responseResult = new ResponseResult();
            if (loginDto == null)
            {
                responseResult.Status = CustomerNotification.Fail;
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Data = null;
                responseResult.Message = CustomerNotification.Login_Null;

                return BadRequest(responseResult);
            }
            else
            {
                var customer = _customerRepository.Login(loginDto);
                if (customer == null)
                {
                    responseResult.Status = CustomerNotification.Fail;
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Data = customer;
                    responseResult.Message = CustomerNotification.Login_Customer_NotActive;

                    return BadRequest(responseResult);
                }
                else if (customer.IsActived == false)
                {
                    responseResult.Status = CustomerNotification.Fail;
                    responseResult.StatusCode = (int)HttpStatusCode.NotFound;
                    responseResult.Data = null;
                    responseResult.Message = CustomerNotification.Login_Fail;

                    return BadRequest(responseResult);
                }
                else
                {
                    responseResult.Status = CustomerNotification.Success;
                    responseResult.StatusCode = (int)HttpStatusCode.OK;
                    responseResult.Data = customer;
                    responseResult.Message = CustomerNotification.Login_Success;

                    return Ok(responseResult);
                }
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> CustomerRegister(RegisterDto registerDto)
        {
            ResponseResult responseResult = new ResponseResult();
            if (registerDto == null)
            {
                responseResult.Status = CustomerNotification.Fail;
                responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                responseResult.Data = null;
                responseResult.Message = CustomerNotification.Register_Data_Null;

                return BadRequest(responseResult);
            }
            else
            {
                if (_customerRepository.GetSingleByCondition(x => x.CustomerEmail == registerDto.Email) != null)
                {
                    responseResult.Status = CustomerNotification.Fail;
                    responseResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseResult.Data = null;
                    responseResult.Message = CustomerNotification.Register_Exist_Email;
                    return BadRequest(responseResult);
                }
                else
                {

                    var customer = _customerRepository.Register(registerDto);
                    if (await _customerRepository.SaveAsync() > 0)
                    {
                        responseResult.Status = CustomerNotification.Success;
                        responseResult.StatusCode = (int)HttpStatusCode.OK;
                        responseResult.Data = customer;
                        responseResult.Message = CustomerNotification.Register_Success;

                        return Ok(responseResult);
                    }
                    else
                    {
                        responseResult.Status = CustomerNotification.Fail;
                        responseResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseResult.Data = customer;
                        responseResult.Message = CustomerNotification.Register_Fail;

                        return BadRequest(responseResult);
                    }
                }
            }
        }
    }
}
