using AutoMapper;
using IS_5.Organization.Model;
using IS_5.Organization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Authorization.Model;
using PetsServer.Organization.Service;
using System.Net;
using System.Security.Claims;
using System.Net;
using System.Security.Claims;

namespace IS_5.Controler
{
    [ApiController]
    [Route("organizations")]
    [Authorize]
    public class OrganizationController : Controller
    {
        // Сервис
        private OrganizationService _service;
        // Для привилегий и доступа
        private UserModel _user;
        // Маппер для данных
        private readonly IMapper _mapper;

        public OrganizationController(IMapper mapper)
        {
            _service = new OrganizationService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetOrganizations")]
        public ActionResult<PageSettings<OrganizationViewList>> GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            _user = TestData.Users.Find(u => u.Login == User.Identity.Name);

            if (!_user.Privilege.Organizations.Item2.Contains(Possibilities.Read))
                return Forbid();

            var pageModel = _service.GetPage(page, pages, filter, sortField, sortType, _user);
            var pageView = new PageSettings<OrganizationViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);
            pageView.Items = _mapper.Map<List<OrganizationViewList>>(pageModel.Items);

            return new ActionResult<PageSettings<OrganizationViewList>>(pageView);
        }

        [HttpGet("{id}", Name = "GetOrganization")]
        public ActionResult<OrganizationViewList> GetOne(int id)
        {
            if (!_user.Privilege.Organizations.Item2.Contains(Possibilities.Read))
                return Forbid();

            var organization = _mapper.Map<OrganizationViewList>(_service.GetOne(id));
            return new ActionResult<OrganizationViewList>(organization);
        }

        [HttpPost(Name = "CreateOrganization")]
        public ActionResult Create([FromBody] OrganizationEdit view)
        {
            if (!_user.Privilege.Organizations.Item2.Contains(Possibilities.Insert))
                return Forbid();

            var organization = _mapper.Map<OrganizationModel>(view);
            _service.Create(organization);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateOrganization")]
        public ActionResult Update(int id, OrganizationEdit view)
        {
            if (!_user.Privilege.Organizations.Item2.Contains(Possibilities.Update))
                return Forbid();

            var organization = _mapper.Map<OrganizationEdit, OrganizationModel>(view);
            organization.Id = id;
            _service.Update(organization);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteOrganization")]
        public ActionResult Delete(int id)
        {
            if (!_user.Privilege.Organizations.Item2.Contains(Possibilities.Delete))
                return Forbid();

            _service.Delete(id);
            return Ok();
        }
    }
}
