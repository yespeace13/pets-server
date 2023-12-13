using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Report.Service;

namespace PetsServer.Domain.Report.Controller
{
    [Route("reports-export")]
    [ApiController]
    [Authorize]
    public class ReportExportController(IMapper mapper) : ControllerBase
    {
        private readonly ReportService _service = new();
        private readonly ReportExcelService _excelService = new();
        private readonly AuthenticationService _authenticationService = new();
        private IMapper _mapper = mapper;

        [HttpGet("{id}", Name = "GenerateReportExcel")]
        public IActionResult Get(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var fileName = $"Report.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = _excelService.GenerateExcel(id);
            return File(file, mimeType, fileName);
        }

        [HttpGet(Name = "GenerateReportsExcel")]
        public IActionResult Get(string? filters)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var fileName = $"Reports.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = _service.ExportToExcel(filters, _mapper);
            return File(file, mimeType, fileName);
        }
    }
}
