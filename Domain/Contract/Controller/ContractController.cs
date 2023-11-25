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
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Table.PivotTable;

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

        public ContractController(IMapper mapper)
        {
            _service = new ContractService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
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
            var view = _mapper.Map<ContractViewOne>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateContract")]
        public IActionResult Create([FromBody] ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<ContractEdit, ContractModel>(view);
            _service.Create(entity);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateContract")]
        public IActionResult Update(int id, ContractEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<ContractEdit, ContractModel>(view);
            entity.Id = id;
            _service.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteContract")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }

        [HttpGet("{contractId}", Name = "GetContractLocalitys")]
        public IActionResult GetLocality(int contractId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var pageModel = _service.GetPage(1, 1000, null, null, null, user, _mapper);

            var pageView = new PageSettings<ContractViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);

            pageView.Items = _mapper.Map<IEnumerable<ContractViewList>>(pageModel.Items);
            return Ok(pageView);
        }
    }
}
