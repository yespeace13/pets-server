using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Report;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Report.Service;

namespace PetsServer.Domain.Report.Controller
{
    [Route("reports")]
    [ApiController]
    // [Authorize]
    public class ReportController : ControllerBase
    {
        private ReportService _service;
        private AuthenticationUserService _authenticationService;
        private IMapper _mapper;
        public ReportController(IMapper mapper)
        {
            _service = new ReportService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpPost(Name = "CreateReport")]
        public IActionResult Create(string from, string to)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Insert, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            if (DateOnly.TryParse(from, out var fromDate) || DateOnly.TryParse(to, out var toDate))
                return BadRequest();
            _service.Create(fromDate, toDate);
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

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var views = _service.Get(page, pages, filter, sortField, sortType, user, _mapper);
            return Ok(views);
        }

        [HttpGet("{id}", Name = "GetReport")]
        public IActionResult Get(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Report, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.Get(id);
            var view = _mapper.Map<ReportViewOne>(entity);
            return Ok(view);
        }
    }
}
