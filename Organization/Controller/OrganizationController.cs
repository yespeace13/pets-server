using IS_5.Organization.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SupportLibrary.Model.Organization;
using SupportLibrary.View;

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
            [FromHeader] int? page,
            [FromHeader] int? pages,
            [FromHeader] string? filter,
            [FromHeader] string? sortField,
            [FromHeader] int? sortType)
        {
            return _service.GetPage(page, pages, filter, sortField, sortType);
        }

        [HttpPost(Name = "CreateOrganization")]
        public void Create([FromBody] OrganizationViewCreate organization) => _service.Create(organization);

        [HttpPut("{id}", Name = "UpdateOrganization")]
        public void Update(int id, OrganizationViewCreate view) => _service.Update(id, view);

        [HttpDelete("{id}", Name = "DeleteOrganization")]
        public void Delete(int id) => _service.Delete(id);

        //public void ExportToExcel(string[] columns, List<FilterSetting> filters)
        //    => _service.ExportToExcel(columns, filters);
    }
}
