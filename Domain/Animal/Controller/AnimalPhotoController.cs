using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Animal.Service;

namespace PetsServer.Domain.Animal.Controller
{
    [ApiController]
    [Route("animals-photo")]
    [Authorize]
    public class AnimalPhotoController : ControllerBase
    {
        // Сервис
        private AnimalPhotoService _service = new AnimalPhotoService();
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService;

        //[HttpGet("{id}", Name = "GetAnimalPhoto")]
        //public IActionResult Get(int id)
        //{
        //    var user = _authenticationService.GetUser(User.Identity.Name);

        //    if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
        //        return Problem(null, null, 403, "У вас нет привилегий");
        //    var entity = _service.GetOne(id);
        //    return Ok(view);
        //}

        [HttpPost("{animalId}", Name = "AddPhoto")]
        public IActionResult AddPhoto(int animalId, [FromBody] byte[] photo)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Insert, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            _service.AddPhoto(animalId, photo);
            return Ok();
        }

        //[HttpDelete("{id}", Name = "DeletePhoto")]
        //public ActionResult DeletePhoto(int id)
        //{
        //    var user = _authenticationService.GetUser(User.Identity.Name);

        //    if (!AuthorizationUserService.IsPossible(Possibilities.Delete, Entities.Act, user))
        //        return Problem(null, null, 403, "У вас нет привилегий");
        //    _service.Delete(id);
        //    return Ok();
        //}
    }
}
