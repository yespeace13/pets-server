using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Organization.Model;
using PetsServer.Organization.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetsServer.Organization.Controller
{
    [Route("legaltype")]
    [ApiController]
    [Authorize]
    public class LegalTypeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<LegalTypeModel> Get()
        {
            return new OrganizationService().GetLegalTypes();
        }
    }
}
