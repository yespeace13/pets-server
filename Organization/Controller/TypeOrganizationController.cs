using Microsoft.AspNetCore.Mvc;
using PetsServer.Organization.Model;
using PetsServer.Organization.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetsServer.Organization.Controller
{
    [Route("typeorganization")]
    [ApiController]
    public class TypeOrganizationController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<TypeOrganizationModel> Get()
        {
            return new OrganizationService().GetTypesOrganization();
        }
    }
}
