using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Contract.Service;

namespace PetsServer.Domain.Contract.Controller;

[Route("сontractexport")]
[ApiController]
public class ContractExportController(IMapper mapper) : ControllerBase
{
    // Маппер для данных
    private readonly IMapper _mapper = mapper;

    private readonly ContractService _service = new();

    [HttpGet]
    public IActionResult Get(string? filters)
    {
        var fileName = $"Contracts.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var file = _service.ExportToExcel(filters, _mapper);
        return File(file, mimeType, fileName);
    }
}

