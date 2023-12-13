using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Plan.Service;

namespace PetsServer.Domain.Plan.Controller;

[Route("plans-export")]
[ApiController]
public class PlanExportController(IMapper mapper) : ControllerBase
{
    // Маппер для данных
    private readonly IMapper _mapper = mapper;

    private PlanService _service = new();

    [HttpGet]
    public IActionResult Get(string? filters)
    {
        var fileName = $"Plans.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var file = _service.ExportToExcel(filters, _mapper);
        return File(file, mimeType, fileName);
    }
}

