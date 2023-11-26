using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Contract.Service;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using ModelLibrary.Model.Etc;
using ModelLibrary.Model.Contract;

namespace PetsServer.Domain.Contract.Controller
{
    [ApiController]
    [Route("contract-contents")]
    [Authorize]
    public class ContractContentController : ControllerBase
    {
        // Сервис
        private ContractContentService _contentService;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public ContractContentController(IMapper mapper)
        {
            _contentService = new ContractContentService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet("{contractId}", Name = "GetContractLocalities")]
        public IActionResult GetContractLocalities(int contractId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var localities = _contentService.GetContractAll(contractId).Select(cc => cc.Locality);
            
            var mapped = _mapper.Map<IEnumerable<LocalityView>>(localities);
            return Ok(mapped);
        }
    }
}
