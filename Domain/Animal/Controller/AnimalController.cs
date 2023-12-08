using AutoMapper;
using ModelLibrary.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Animal.Service;
using ModelLibrary.Model.Animal;
using PetsServer.Domain.Animal.Model;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Animal.Controller
{
    [ApiController]
    [Route("animals")]
    [Authorize]
    public class AnimalController : ControllerBase
    {
        // Сервис
        private AnimalService _service;
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;
        // Маппер для данных
        private readonly IMapper _mapper;

        public AnimalController(IMapper mapper)
        {
            _service = new AnimalService();
            _authenticationService = new AuthenticationUserService();
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAnimals")]
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

            var pageView = new PageSettings<AnimalViewList>(pageModel.Pages, pageModel.Page, pageModel.Limit);

            pageView.Items = _mapper.Map<IEnumerable<AnimalViewList>>(pageModel.Items);
            return Ok(pageView);
        }

        [HttpGet("{id}", Name = "GetAnimal")]
        public IActionResult GetOne(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _service.GetOne(id);
            var view = _mapper.Map<AnimalViewList>(entity);
            return Ok(view);
        }

        [HttpPost(Name = "CreateAnimal")]
        public IActionResult Create([FromBody] AnimalEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var entity = _mapper.Map<AnimalEdit, AnimalModel>(view);
            _service.Create(entity);
            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateAnimal")]
        public IActionResult Update(int id, AnimalEdit view)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            var entity = _mapper.Map<AnimalEdit, AnimalModel>(view);
            entity.Id = id;
            _service.Update(entity);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteAnimal")]
        public ActionResult Delete(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Contract, user))
                return Problem(null, null, 403, "У вас нет привилегий");

            _service.Delete(id);
            return Ok();
        }
    }
}
