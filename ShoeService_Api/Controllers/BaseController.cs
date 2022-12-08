using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoeService_Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
