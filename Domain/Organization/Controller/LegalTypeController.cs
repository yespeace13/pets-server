using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Organization.Service;

namespace PetsServer.Domain.Organization.Controller;

[Route("legaltype")]
[ApiController]
[Authorize]
public class LegalTypeController : ControllerBase
{
    private readonly OrganizationService _organizationService = new();

    [HttpGet(Name = "GetLegalTypes")]
    public IActionResult GetAll() => Ok(_organizationService.GetLegalTypes());


    [HttpGet("{id}", Name = "GetLegalType")]
    public IActionResult GetOne(int id) => Ok(_organizationService.GetLegalType(id));
}
