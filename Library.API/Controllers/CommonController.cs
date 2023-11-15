using Library.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class CommonController : ControllerBase
    {
        protected string? UserLogin => User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

        protected ActionResult Result(ServiceResult value)
        {
            if (value.Success)
                return Ok(value.Message);
            return BadRequest(value.Message);
        }
    }
}
