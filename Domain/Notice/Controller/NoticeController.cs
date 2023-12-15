using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;

namespace PetsServer.Domain.Notice.Controller
{
    [Route("notices")]
    [ApiController]
    [Authorize]
    public class NoticeController : ControllerBase
    {
        private readonly NoticeService _service = new();
        private readonly AuthenticationService _authenticationService = new();

        [HttpGet("reports", Name = "GetReportsNotice")]
        public IActionResult Get()
        {
            var user = _authenticationService.GetUser(User.Identity.Name);
            var notices = _service.GetReportsNotice(user);
            return Ok(notices);
        }
    }
}
