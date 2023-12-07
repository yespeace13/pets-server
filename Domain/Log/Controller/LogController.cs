using AutoMapper;
using ModelLibrary.Model.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Log.Service;
using PetsServer.Domain.Log.Model;
using ModelLibrary.Model.Papka;
using ModelLibrary.View;

namespace PetsServer.Domain.Log.Controller
{
    [ApiController]
    [Route("logs")]
    [Authorize]
    public class LogController : ControllerBase
    {
        // Сервис
        private LogService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public LogController(IMapper mapper)
        {
            _service = new LogService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetLogs")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageModel = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            var pageView = new PageSettings<LogViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);
            pageView.Items = _mapper.Map<List<LogViewList>>(pageModel.Items);

            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetLog")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var model = _service.GetOne(id);
            var view = _mapper.Map<LogViewList>(model);
            return Ok(view);

        }


        [HttpDelete("{id}", Name = "DeleteLog")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Delete, Entities.Organization, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }
    }
}
