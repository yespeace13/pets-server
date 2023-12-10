using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Log.Controller;

[Route("logs-export")]
[ApiController]
public class LogExportController(IMapper mapper) : ControllerBase
{

    // Маппер для данных
    private readonly IMapper _mapper = mapper;

    private readonly LogService _service = new();

    [HttpGet]
    public IActionResult Get(string? filters)
    {
        var fileName = $"Журнал.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var file = _service.ExportToExcel(filters, _mapper);
        return File(file, mimeType, fileName);
    }
}

