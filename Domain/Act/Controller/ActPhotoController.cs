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
    [Route("act-photo")]
    [Authorize]
    public class ActPhotoController : ControllerBase
    {
        // Сервис
        private readonly ActPhotoService _service = new();
        // Для привилегий и доступа
        private readonly AuthenticationService _authenticationService = new();
        private readonly LogService _log = new(typeof(ActPhoto));

        [HttpGet("{animalId}", Name = "GetActPhotos")]
        public IActionResult Get(int animalId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var photos = _service.Get(animalId);
            return Ok(photos);
        }

        [HttpPost("{actId}", Name = "AddActPhoto")]
        public IActionResult AddPhoto(int actId, IFormFile file)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var photoId = _service.AddPhoto(actId, file);
            _log.Log(user, actId, photoId);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteActPhoto")]
        public ActionResult DeletePhoto(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var actId = _service.GetActIdByPhotoId(id);
            _service.DeletePhoto(id);
            _log.Log(user, actId, id);
            return Ok();
        }
    }
}
