using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Model.Contract;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Service;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using FluentValidation;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Contract.Controller
{
    [ApiController]
    [Route("contracts")]
    [Authorize]
    public class ContractController(IMapper mapper, IValidator<ContractModel> validator) : ControllerBase
    {
        // Сервис
        private readonly ContractService _service = new();
        // Для привилегий и доступа
        private readonly AuthenticationService _authenticationService = new();
        // Маппер для данных
        private readonly IMapper _mapper = mapper;
        // Валидатор
        private readonly IValidator<ContractModel> _validator = validator;
        // Логгер
        private readonly LoggerFacade _log = new(new DbLogger(), new TxtLogger());

        [HttpGet(Name = "GetContracts")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageView = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetContract")]
        public IActionResult Get(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.Get(id);

            if (entity == null) return BadRequest("Контракт не найден");

            var view = _mapper.Map<ContractViewOne>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateContract")]
        public IActionResult Create([FromBody] ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<ContractEdit, ContractModel>(view);

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
                return BadRequest(String.Join('\n', validationResult.Errors.Select(e => e.ErrorMessage)));
            
            var id = _service.Create(entity);
            _log.Log(user,Entities.Contract, Possibilities.Insert, typeof(ContractModel), id);
            return Ok(id);

        }


        [HttpPut("{id}", Name = "UpdateContract")]
        public IActionResult Update(int id, ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Update, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<ContractEdit, ContractModel>(view);
            entity.Id = id;

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
                return BadRequest(String.Join('\n', validationResult.Errors.Select(e => e.ErrorMessage)));
            
            _service.Update(entity);
            _log.Log(user, Entities.Contract, Possibilities.Update, typeof(ContractModel), id);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteContract")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            _log.Log(user, Entities.Contract, Possibilities.Delete, typeof(ContractModel), id);
            return Ok();
        }
    }
}
