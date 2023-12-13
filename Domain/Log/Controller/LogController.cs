using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Log.Service;
using ModelLibrary.View;
using ModelLibrary.Model.LogInformation;

namespace PetsServer.Domain.Log.Controller
{
    [ApiController]
    [Route("logs")]
    [Authorize]
    public class LogController(IMapper mapper) : ControllerBase
    {
        // Сервис
        private readonly LogService _service = new();
        // Для привилегий и доступа
        private readonly AuthenticationService _authenticationService = new();
        // Маппер для данных
        private readonly IMapper _mapper = mapper;

        [HttpGet(Name = "GetLogs")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Log, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageView = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetLog")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Log, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var model = _service.GetOne(id);
            var view = _mapper.Map<LogViewList>(model);
            return Ok(view);

        }


        [HttpDelete("{id}", Name = "DeleteLog")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Log, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }
    }
}
