using IS_5.Organization.Service;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Organization.Model;

namespace IS_5.Controler
{
    [ApiController]
    [Route("organizations")]
    public class OrganizationController : Controller
    {
        private OrganizationService _service;

        public OrganizationController()
        {
            _service = new OrganizationService();
        }

        [HttpGet(Name = "GetOrganizations")]
        public PageSettings<OrganizationViewList> GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            return _service.GetPage(page, pages, filter, sortField, sortType);
        }
        //[HttpGet(Name = "GetTypeOrganizations")]
        //public List<TypeOrganizationModel> GetTypesOrganization()
        //{
        //    return _service.GetTypesOrganization();
        //}
        //[HttpGet(Name = "GetLegalTypes")]
        //public List<LegalTypeModel> GetLegalTypes()
        //{
        //    return _service.GetLegalTypes();
        //}

        [HttpPost(Name = "CreateOrganization")]
        public void Create([FromBody] OrganizationEdit organization) => _service.Create(organization);

        [HttpPut("{id}", Name = "UpdateOrganization")]
        public void Update(int id, OrganizationEdit view) => _service.Update(id, view);

        [HttpDelete("{id}", Name = "DeleteOrganization")]
        public void Delete(int id) => _service.Delete(id);

            
    }
}
