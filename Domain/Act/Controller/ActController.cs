using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Act.Service;
using ModelLibrary.Model.Act;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Act.Controller
{
    [ApiController]
    [Route("acts")]
    [Authorize]
    public class ActController(IMapper mapper) : ControllerBase
    {
        // Сервис
        private readonly ActService _service = new();
        // Для привилегий и доступа
        private readonly AuthenticationService _authenticationService = new();
        // Маппер для данных
        private readonly IMapper _mapper = mapper;
        private readonly LoggerFacade _log = new(new DbLogger(), new TxtLogger());

        [HttpGet(Name = "GetActs")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageView = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetAct")]
        public IActionResult Get(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.GetOne(id);
            var view = _mapper.Map<ActModel, ActViewOne>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateAct")]
        public IActionResult Create([FromBody] ActEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<ActEdit, ActModel>(view);
            var id = _service.Create(entity);
            _log.Log(user, Entities.Act, Possibilities.Insert, typeof(ActModel), id);
            return Ok(id);

        }

        [HttpPut("{id}", Name = "UpdateAct")]
        public IActionResult Update(int id, ActEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<ActEdit, ActModel>(view);
            entity.Id = id;
            _service.Update(entity);
            _log.Log(user, Entities.Act, Possibilities.Update, typeof(ActModel), id);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteAct")]
        public IActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            _log.Log(user, Entities.Act, Possibilities.Delete, typeof(ActModel), id);
            return Ok();
        }
    }
}
