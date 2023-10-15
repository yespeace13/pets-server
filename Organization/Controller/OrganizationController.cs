using IS_5.Organization.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Authorization.Model;
using PetsServer.Organization.Model;
using System.Net;
using System.Security.Claims;

namespace IS_5.Controler
{
    [ApiController]
    [Route("organizations")]
    [Authorize]
    public class OrganizationController : Controller
    {
        private OrganizationService _service;
        private UserModel _user;

        public OrganizationController()
        {
            _service = new OrganizationService();
            
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
            return new ActionResult<PageSettings<OrganizationViewList>>(_service.GetPage(page, pages, filter, sortField, sortType, _user));
        }

        [HttpGet("{id}", Name = "GetOrganization")]
        public ActionResult<OrganizationViewList> GetOne(int id)
        {
            return new ActionResult<OrganizationViewList>(_service.GetOne(id));
        }

        [HttpPost(Name = "CreateOrganization")]
        public void Create([FromBody] OrganizationEdit organization) => _service.Create(organization);

        [HttpPut("{id}", Name = "UpdateOrganization")]
        public void Update(int id, OrganizationEdit view) => _service.Update(id, view);

        [HttpDelete("{id}", Name = "DeleteOrganization")]
        public void Delete(int id) => _service.Delete(id);


    }
}
