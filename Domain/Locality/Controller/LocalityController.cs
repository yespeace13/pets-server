using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Locality.Service;

namespace PetsServer.Domain.Locality.Controller;

[ApiController]
[Route("localities")]
[Authorize]
public class LocalityController : ControllerBase
{
    [HttpGet(Name = "GetLocalities")]
    public IActionResult GetAll()
    {
        return Ok(new LocalityService().GetLocalitys());
    }

    [HttpGet("{id}", Name = "GetLocality")]
    public IActionResult GetOne(int id)
    {
        return Ok(GetOne(id));
    }
}
