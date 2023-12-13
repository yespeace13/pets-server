using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Service;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Contract.Controller;

[ApiController]
[Route("contract-photo")]
[Authorize]
public class ContractPhotoController : ControllerBase
{
    // Сервис
    private readonly ContractPhotoService _service = new();
    // Для привилегий и доступа
    private readonly AuthenticationService _authenticationService = new();
    private readonly LoggerFacade _log = new (new DbLogger(), new TxtLogger());

    [HttpGet("{contractId}", Name = "GetContractPhotos")]
    public IActionResult Get(int contractId)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Contract, user))
            return Problem(null, null, 403, "У вас нет привилегий");
        var photos = _service.Get(contractId);
        return Ok(photos);
    }

    [HttpPost("{contractId}", Name = "AddContractPhoto")]
    public IActionResult AddPhoto(int contractId, IFormFile file)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Contract, user))
            return Problem(null, null, 403, "У вас нет привилегий");
        var photoId = _service.AddPhoto(contractId, file);
        _log.Log(user, Entities.Contract, Possibilities.Insert, typeof(ContractPhoto), contractId, photoId);
        return Ok();
    }

    [HttpDelete("{id}", Name = "DeleteContractPhoto")]
    public ActionResult DeletePhoto(int id)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Contract, user))
            return Problem(null, null, 403, "У вас нет привилегий");

        var contractId = _service.GetContractIdByPhotoId(id);
        _service.DeletePhoto(id);
        _log.Log(user, Entities.Contract, Possibilities.Delete, typeof(ContractModel), contractId, id);
        return Ok();
    }
}
