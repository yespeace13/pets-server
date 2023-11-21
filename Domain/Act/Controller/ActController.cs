using AutoMapper;
using ModelLibrary.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Act.Service;
using ModelLibrary.Model.Act;
using PetsServer.Domain.Act.Model;
using System.Collections.Generic;
using ModelLibrary.Model.Contract;

namespace PetsServer.Domain.Act.Controller
{
    [ApiController]
    [Route("acts")]
    [Authorize]
    public class ActController : ControllerBase
    {
        // Сервис
        private ActService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public ActController(IMapper mapper)
        {
            _service = new ActService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetActs")]
        public IActionResult GetPage(
            int? page,
            int? pages,
            string? filter,
            string? sortField,
            int? sortType)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var pageModel = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

            var pageView = new PageSettings<ActViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);

            //pageView.Items = _mapper.Map<IEnumerable<ActModel>, IEnumerable<ActViewList>>(pageModel.Items);
            pageView.Items = pageModel.Items;
            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetAct")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.GetOne(id);
            var view = _mapper.Map<ActModel, ActViewOne>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateAct")]
        public IActionResult Create([FromBody] ActEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<ActEdit, ActModel>(view);
            _service.Create(entity);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateAct")]
        public IActionResult Update(int id, ActEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<ActEdit, ActModel>(view);
            entity.Id = id;
            _service.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteAct")]
        public IActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }
    }
}
