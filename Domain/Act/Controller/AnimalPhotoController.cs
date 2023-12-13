using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Act.Service;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Act.Controller
{
    [ApiController]
    [Route("animal-photo")]
    [Authorize]
    public class AnimalPhotoController : ControllerBase
    {
        // Сервис
        private readonly AnimalPhotoService _service = new();
        // Для привилегий и доступа
        private readonly AuthenticationService _authenticationService = new();
        private readonly LoggerFacade _logger = new(new DbLogger(), new TxtLogger());

        [HttpGet("{animalId}", Name = "GetAnimalPhotos")]
        public IActionResult Get(int animalId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var photos = _service.Get(animalId);
            return Ok(photos);
        }

        [HttpPost("{animalId}", Name = "AddPhoto")]
        public IActionResult AddPhoto(int animalId, IFormFile file)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var id = _service.AddPhoto(animalId, file);
            _logger.Log(user, Entities.AnimalPhoto, Possibilities.Insert, typeof(AnimalPhoto), animalId, id);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeletePhoto")]
        public ActionResult DeletePhoto(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var animalId = _service.DeletePhoto(id);
            _logger.Log(user, Entities.AnimalPhoto, Possibilities.Delete, typeof(AnimalPhoto), animalId, id);
            return Ok();
        }
    }
}
