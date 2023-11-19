using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelLibrary.Model.Authentication;
using PetsServer.Auth.Authorization.Model;

namespace PetsServer.Auth.Authentication
{
    [Route("authentication")]
    [ApiController]
    public class AuthenticationUserController : ControllerBase
    {
        private AuthenticationUserService _authenticationService;

        private IMapper _mapper;
        public AuthenticationUserController(IMapper mapper)
        {
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }


        [HttpPost(Name = "CreteUser")]
        public IActionResult Create([FromBody] UserEdit view)
        {
            var user = _mapper.Map<UserModel>(view);
            if (ModelState.IsValid)
            {
                _authenticationService.CreateUser(user);
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet(Name = "GetUsers")]
        public IActionResult GetAll()
        {
            var currentUser = _authenticationService.GetUser(User.Identity.Name);
            if (currentUser != null && currentUser.Login == "super")
            {
                var users = _authenticationService.GetUsers();
                return Ok(users);
            }
            return Problem(null, null, 403, "У вас нет привилегий");
        }
    }
}
