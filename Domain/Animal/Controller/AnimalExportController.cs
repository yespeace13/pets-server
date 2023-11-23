using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Animal.Service;

namespace PetsServer.Domain.Animal.Controller;

[Route("animalsexport")]
[ApiController]
public class AnimalExportController : ControllerBase
{

    // Маппер для данных
    private readonly IMapper _mapper;

    private AnimalService _service;

    public AnimalExportController(IMapper mapper)
    {
        _service = new AnimalService();
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(string? filters)
    {
        var fileName = $"Животные.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var file = _service.ExportToExcel(filters, _mapper);
        return File(file, mimeType, fileName);
    }
}

