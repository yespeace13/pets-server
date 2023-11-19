using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Organization.Service;

namespace PetsServer.Domain.Organization.Controller
{
    [Route("typeorganization")]
    [ApiController]
    public class TypeOrganizationController : ControllerBase
    {
        private OrganizationService _organizationService;

        public TypeOrganizationController() => _organizationService = new OrganizationService();

        [HttpGet(Name = "GetTypesOrganization")]
        public IActionResult GetAll() => Ok(_organizationService.GetTypesOrganization());


        [HttpGet("{id}", Name = "GetTypeOrganization")]
        public IActionResult GetOne(int id) => Ok(_organizationService.GetTypeOrganization(id));
    }
}
