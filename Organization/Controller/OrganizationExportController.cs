using Microsoft.AspNetCore.Mvc;
using PetsServer.Organization.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetsServer.Organization.Controller
{
    [Route("organizationexport")]
    [ApiController]
    public class OrganizationExportController : ControllerBase
    {

        // Маппер для данных
        private readonly IMapper _mapper;

        private OrganizationService _service;

        public OrganizationExportController(IMapper mapper)
        {
            _service = new OrganizationService();
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(string? filters)
        {
            var fileName = $"Организации.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = _service.ExportToExcel(filters);
            return File(file, mimeType, fileName);
        }
    }
}
