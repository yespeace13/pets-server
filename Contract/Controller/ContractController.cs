using AutoMapper;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Authorization.Model;
using PetsServer.Organization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Authentication;
using PetsServer.Contract.Service;
using ModelLibrary.Contract;
using PetsServer.Contract.Model;

namespace PetsServer.Organization.Controller
{
    [ApiController]
    [Route("contract")]
    [Authorize]
    public class ContractController : ControllerBase
    {
        // Сервис
        private ContractService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public ContractController(IMapper mapper)
        {
            _service = new ContractService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetContracts")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageModel = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            var pageView = new PageSettings<OrganizationViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);

            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetContract")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var organization = _mapper.Map<ContractViewOne>(_service.GetOne(id));
            return Ok(organization);

        }

        [HttpPost(Name = "CreateContract")]
        public IActionResult Create([FromBody] ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var organization = _mapper.Map<ContractEdit, ContractModel>(view);
            _service.Create(organization);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateContract")]
        public IActionResult Update(int id, ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var organization = _mapper.Map<ContractEdit, ContractModel>(view);
            organization.Id = id;
            _service.Update(organization);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteContract")]
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
