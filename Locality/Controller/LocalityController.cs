using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Locality.Repository;

namespace PetsServer.Locality.Controller
{
    [ApiController]
    [Route("localitys")]
    [Authorize]
    public class LocalityController : ControllerBase
    {
        [HttpGet(Name = "GetLocalitys")]
        public IActionResult GetAll()
        {
            return Ok(new LocalityRepository().GetLocalitys());
        }

        [HttpGet("{id}", Name = "GetLocality")]
        public IActionResult GetOne(int id)
        {
            return Ok(GetOne(id));
        }
    }
}
