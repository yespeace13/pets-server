using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Domain.Report.Service;

namespace PetsServer.Domain.Report.Controller
{
    [Route("reports-excel")]
    [ApiController]
    // [Authorize]
    public class ReportExportController : ControllerBase
    {
        private ReportService _service;
        private AuthenticationUserService _authenticationService;
        private IMapper _mapper;
        public ReportExportController(IMapper mapper)
        {
            _service = new ReportService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GenerateReportExcel")]
        public IActionResult Get(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            //if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Report, user))
            //    return Problem(null, null, 403, "У вас нет привилегий");

            var fileName = $"Report.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = _service.GenerateExcel(id);
            return File(file, mimeType, fileName);
        }

        [HttpGet(Name = "GenerateReportsExcel")]
        public IActionResult Get(string? filters)
        {
            var fileName = $"Reports.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var file = _service.ExportToExcel(filters, _mapper);
            return File(file, mimeType, fileName);
        }
    }
}
