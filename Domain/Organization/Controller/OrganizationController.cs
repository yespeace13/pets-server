using AutoMapper;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Organization.Model;
using PetsServer.Domain.Organization.Service;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Organization.Controller
{
    [ApiController]
    [Route("organizations")]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        // Сервис
        private OrganizationService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public OrganizationController(IMapper mapper)
        {
            _service = new OrganizationService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetOrganizations")]
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

            var pageView = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            //var pageView = new PageSettings<OrganizationViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);
            //pageView.Items = _mapper.Map<List<OrganizationViewList>>(pageModel.Items);

            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetOrganization")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var organization = _mapper.Map<OrganizationViewList>(_service.GetOne(id));
            return Ok(organization);

        }

        [HttpPost(Name = "CreateOrganization")]
        public IActionResult Create([FromBody] OrganizationEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Insert, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var organization = _mapper.Map<OrganizationModel>(view);
            _service.Create(organization);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateOrganization")]
        public IActionResult Update(int id, OrganizationEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Update, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var organization = _mapper.Map<OrganizationEdit, OrganizationModel>(view);
            organization.Id = id;
            _service.Update(organization);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteOrganization")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Delete, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }
    }
}
