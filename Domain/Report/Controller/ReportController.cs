using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Report;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Log.Service;
using PetsServer.Domain.Report.Model;
using PetsServer.Domain.Report.Service;

namespace PetsServer.Domain.Report.Controller
{
    [Route("reports")]
    [ApiController]
    [Authorize]
    public class ReportController(IMapper mapper) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ReportService _service = new();
        private readonly AuthenticationService _authenticationService = new();
        private readonly LoggerFacade _logger = new(new DbLogger(), new TxtLogger());

        [HttpPost(Name = "CreateReport")]
        public IActionResult Create(string from, string to)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            if (!DateOnly.TryParse(from, out var fromDate) || !DateOnly.TryParse(to, out var toDate))
                return BadRequest("Не правильный формат дат");
            var id = _service.Create(fromDate, toDate);
            _logger.Log(user, Entities.Report, Possibilities.Insert, typeof(ReportModel), id);
            return Ok();
        }

        [HttpGet(Name = "GetReports")]
        public IActionResult Get(int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var views = _service.Get(page, pages, filter, sortField, sortType, user, _mapper);
            return Ok(views);
        }

        [HttpGet("{id}", Name = "GetReport")]
        public IActionResult Get(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.Get(id);
            var view = _mapper.Map<ReportViewOne>(entity);
            return Ok(view);
        }

        [HttpDelete("{id}", Name = "DeleteReport")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            _logger.Log(user, Entities.Report, Possibilities.Delete, typeof(ReportModel), id);
            return Ok();
        }

        [HttpPost("{reportId}/statuses/{statusId}",Name = "SetReportStatus")]
        public IActionResult SetReportStatus(int reportId, int statusId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.ChangeStatus, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            
            _service.SetReportStatus(reportId, statusId);
            _logger.Log(user, Entities.Report, Possibilities.ChangeStatus, typeof(ReportModel), reportId);
            return Ok();
        }

        [HttpGet("{reportId}/statuses", Name = "GetReportStatuses")]
        public IActionResult GetStatuses(int reportId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);
            var statuses = _service.GetStatuses(reportId, user);
            return Ok(statuses);
        }

        [HttpGet("{reportId}/actual-status", Name = "GetReportActualStatuses")]
        public IActionResult GetActualStatus(int reportId)
        {
            var status = _service.GetActualStatus(reportId);
            return Ok(status);
        }
    }
}
