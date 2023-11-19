using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Service;

namespace PetsServer.Auth.Authorization.Controller
{
    [ApiController]
    [Route("authorization")]

    // Эта вся штука нужна только для того, чтобы быстро добавить пользователю роль
    // Только super-man может что-то с этим делать:)
    public class AuthorizationUserController : ControllerBase
    {
        private AuthenticationUserService _authenticationService;

        private AuthorizationUserService _authorizationUserService;

        public AuthorizationUserController()
        {
            _authenticationService = new AuthenticationUserService();
            _authorizationUserService = new AuthorizationUserService();
        }

        [Authorize]
        [HttpPost(Name = "AddUserRole")]
        public IActionResult AddUserRole(int userId, int roleId)
        {
            var currentUser = _authenticationService.GetUser(User.Identity.Name);
            if (currentUser != null && currentUser.Login == "super")
            {
                _authorizationUserService.AddUserRole(userId, roleId);
                return Ok();
            }
            return Problem(null, null, 403, "У вас нет привилегий");
        }

        [Authorize]
        [HttpPost("{userId}", Name = "DeleteUserRole")]
        public IActionResult DeleteUserRole(int userId)
        {
            var currentUser = _authenticationService.GetUser(User.Identity.Name);
            if (currentUser != null && currentUser.Login == "super")
            {
                _authorizationUserService.DeleteUserRole(userId);
                return Ok();
            }
            return Problem(null, null, 403, "У вас нет привилегий");
        }

        [HttpGet("{userId}", Name = "GetUserPriviliges")]
        public IActionResult GetUserPossipilities(int userId)
        {
            return Ok(_authorizationUserService.GetUserPossibilities(userId));
        }
    }
}
