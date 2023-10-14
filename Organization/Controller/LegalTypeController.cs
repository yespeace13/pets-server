using IS_5.Organization.Service;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Organization.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetsServer.Organization.Controller
{
    [Route("legaltype")]
    [ApiController]
    public class LegalTypeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<LegalTypeModel> Get()
        {
            return new OrganizationService().GetLegalTypes();
        }
    }
}
