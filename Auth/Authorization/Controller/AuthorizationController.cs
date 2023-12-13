using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Authentication;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;

namespace PetsServer.Auth.Authorization.Controller;

[ApiController]
[Route("authorization")]

// Эта вся штука нужна только для того, чтобы быстро добавить пользователю роль
// Только super-man может что-то с этим делать:)
public class AuthorizationController(IMapper mapper) : ControllerBase
{
    private readonly AuthenticationService _authenticationService = new();

    private readonly AuthorizationService _authorizationService = new();

    private IMapper _mapper = mapper;

    [Authorize]
    [HttpPost(Name = "AddUserRole")]
    public IActionResult AddUserRole(int userId, int roleId)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);
        if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Authorization, user))
            return Problem(null, null, 403, "У вас нет привилегий");

        _authorizationService.AddUserRole(userId, roleId);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{userId}", Name = "DeleteUserRole")]
    public IActionResult DeleteUserRole(int userId)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);
        if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Authorization, user))
            return Problem(null, null, 403, "У вас нет привилегий");

        _authorizationService.DeleteUserRole(userId);
        return Ok();
    }

    [HttpGet(Name = "GetUserPriviliges")]
    public IActionResult GetUserPossipilities()
    {
        var currentUser = _authenticationService.GetUser(User.Identity.Name);
        var userPossibilities = _authorizationService.GetUserPriviliges(currentUser.Id);
        var view = _mapper.Map<List<EntityPossibilities>, List<UserPossibilities>>(userPossibilities);
        return Ok(view);
    }
}
