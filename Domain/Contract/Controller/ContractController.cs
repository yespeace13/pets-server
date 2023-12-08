using AutoMapper;
using ModelLibrary.View;
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
    public class ContractController : ControllerBase
    {
        // Сервис
        private ContractService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;
        // Валидатор
        private readonly IValidator<ContractModel> _validator;
        // Логгер
        private LogService d_log = new LogService(typeof(ContractModel));

        public ContractController(IMapper mapper, IValidator<ContractModel> validator)
        {
            _service = new ContractService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet(Name = "GetContracts")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageModel = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            var pageView = new PageSettings<ContractViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);

            pageView.Items = _mapper.Map<IEnumerable<ContractViewList>>(pageModel.Items);
            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetContract")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.GetOne(id);

            if (entity == null) return BadRequest("Контракт не найден");

            var view = _mapper.Map<ContractViewOne>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateContract")]
        public IActionResult Create([FromBody] ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Insert, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<ContractEdit, ContractModel>(view);

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return BadRequest(String.Join('\n', validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            var id = _service.Create(entity);
            d_log.LogData(user, id);
            return Ok(id);

        }


        [HttpPut("{id}", Name = "UpdateContract")]
        public IActionResult Update(int id, ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Update, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<ContractEdit, ContractModel>(view);
            entity.Id = id;

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                return BadRequest(String.Join('\n', validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            _service.Update(entity);
            d_log.LogData(user, id);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteContract")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Delete, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            d_log.LogData(user, id);
            return Ok();
        }
    }
}
