using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoeService_Api.Notifications;
using ShoeService_Data.Repository;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System.Net;

namespace ShoeService_Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ShoeController : Controller
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IMapper _mapper;

        public ShoeController(IShoeRepository shoeRepository, IMapper mapper)
        {
            _shoeRepository = shoeRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(ShoeDto shoeDto)
        {
            ResponseResult responseResult = new ResponseResult();

            if (shoeDto != null)
            {
                var shoe = _mapper.Map<Shoe>(shoeDto);
                _shoeRepository.Add(shoe);
                if (await _shoeRepository.SaveAsync() >= 0)
                {
                    responseResult.StatusCode = (int)HttpStatusCode.OK;
                    responseResult.Status = ShoeNotification.Success;
                    responseResult.Message = ShoeNotification.Create_Success;
                    responseResult.Data = shoeDto;

                    return Ok();
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
    }
}
