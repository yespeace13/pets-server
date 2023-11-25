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
    [Authorize]
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
        public IActionResult Create([FromQuery]DateOnly from, [FromQuery] DateOnly to)
        {
            _service.Create(from, to);
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
            var entity = _service.Get();
            var view = _mapper.Map<IEnumerable<ReportViewList>>(entity);
            return Ok(view);
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
