using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Authentication;
using PetsServer.Organization.Service;

namespace PetsServer.Controler
{
    [ApiController]
    [Route("authorization")]
    [Authorize]
    // Эта вся штука нужна только для того, чтобы быстро добавить пользователю роль
    public class AuthorizationUserController : ControllerBase
    {
        private AuthenticationUserService _authenticationService;

        private AuthorizationUserService _authorizationUserService;

        public AuthorizationUserController()
        {
            _authenticationService = new AuthenticationUserService();
            _authorizationUserService = new AuthorizationUserService();
        }


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
    }
}
