using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Contract.Service;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using ModelLibrary.Model.Etc;

namespace PetsServer.Domain.Contract.Controller;

[ApiController]
[Route("contract-contents")]
[Authorize]
public class ContractContentController(IMapper mapper) : ControllerBase
{
    // Сервис
    private readonly ContractContentService _contentService = new();
    // Для привилегий и доступа
    private readonly AuthenticationService _authenticationService = new();
    // Маппер для данных
    private readonly IMapper _mapper = mapper;

    [HttpGet("{contractId}", Name = "GetContractLocalities")]
    public IActionResult GetContractLocalities(int contractId)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Contract, user))
            return Problem(null, null, 403, "У вас нет привилегий");
        var localities = _contentService.GetAllContentByContractId(contractId).Select(cc => cc.Locality);
        
        var mapped = _mapper.Map<IEnumerable<LocalityView>>(localities);
        return Ok(mapped);
    }
}
