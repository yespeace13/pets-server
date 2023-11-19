using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Contract;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Service;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;

namespace PetsServer.Domain.Contract.Controller
{
    [ApiController]
    [Route("contract/{contractId}/contract-content")]
    [Authorize]
    public class ContractContentController : ControllerBase
    {
        // Сервис
        private ContractContentService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public ContractContentController(IMapper mapper)
        {
            _service = new ContractContentService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetContractContents")]
        public IActionResult GetAll(int contractId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.GetAll(contractId);
            var view = _mapper.Map<IEnumerable<ContractContentView>>(entity);
            return Ok(view);
        }

        [HttpGet("{id}", Name = "GetContractContent")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.GetOne(id);
            var view = _mapper.Map<ContractContentView>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateContractContent")]
        public IActionResult Create([FromBody] ContractContentEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<ContractContentEdit, ContractContentModel>(view);
            _service.Create(entity);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateContractContent")]
        public IActionResult Update(int id, ContractContentEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<ContractContentEdit, ContractContentModel>(view);
            entity.Id = id;
            _service.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteContractContent")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }
    }
}
