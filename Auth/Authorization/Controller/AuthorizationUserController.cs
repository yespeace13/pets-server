using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Authentication;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
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

        private IMapper _mapper;

        public AuthorizationUserController(IMapper mapper)
        {
            _authenticationService = new AuthenticationUserService();
            _authorizationUserService = new AuthorizationUserService();
            _mapper = mapper;
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
        [HttpDelete("{userId}", Name = "DeleteUserRole")]
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

        [HttpGet(Name = "GetUserPriviliges")]
        public IActionResult GetUserPossipilities()
        {
            var currentUser = _authenticationService.GetUser(User.Identity.Name);
            if (currentUser != null)
            {
                var userPossibilities = _authorizationUserService.GetUserPriviliges(currentUser.Id);
                var view = _mapper.Map<List<EntityPossibilities>, List<UserPossibilities>>(userPossibilities);
                return Ok(view);
            }
            return Problem(null, null, 403, "У вас нет привилегий");
        }
    }
}
