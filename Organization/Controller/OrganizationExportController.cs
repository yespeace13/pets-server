using Microsoft.AspNetCore.Mvc;
using PetsServer.Organization.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetsServer.Organization.Controller
{
    [Route("organizationexport")]
    [ApiController]
    public class OrganizationExportController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string? filters)
        {
            var fileName = $"Организации.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = new OrganizationService().ExportToExcel(filters);
            return File(file, mimeType, fileName);
        }
    }
}
