﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Contract.Service;

namespace PetsServer.Domain.Contract.Controller
{
    [ApiController]
    [Route("contract-photo")]
    [Authorize]
    public class ContractPhotoController : ControllerBase
    {
        // Сервис
        private ContractPhotoService _service = new ContractPhotoService();
        // Для привилегий и доступа
        private AuthenticationUserService _authenticationService = new AuthenticationUserService();

        [HttpGet("{contractId}", Name = "GetContractPhotos")]
        public IActionResult Get(int contractId)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Read, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            var photos = _service.Get(contractId);
            return Ok(photos);
        }

        [HttpPost("{contractId}", Name = "AddContractPhoto")]
        public IActionResult AddPhoto(int contractId, IFormFile file)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Insert, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            _service.AddPhoto(contractId, file);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteContractPhoto")]
        public ActionResult DeletePhoto(int id)
        {
            var user = _authenticationService.GetUser(User.Identity.Name);

            if (!AuthorizationUserService.IsPossible(Possibilities.Delete, Entities.Act, user))
                return Problem(null, null, 403, "У вас нет привилегий");
            _service.DeletePhoto(id);
            return Ok();
        }
    }
}
