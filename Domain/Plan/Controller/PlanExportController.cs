using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Plan.Service;

namespace PetsServer.Domain.Plan.Controller;

[Route("plans-excel")]
[ApiController]
public class PlanExportController : ControllerBase
{

    // Маппер для данных
    private readonly IMapper _mapper;

    private PlanService _service;

    public PlanExportController(IMapper mapper)
    {
        _service = new PlanService();
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(string? filters)
    {
        var fileName = $"Plans.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var file = _service.ExportToExcel(filters, _mapper);
        return File(file, mimeType, fileName);
    }
}

