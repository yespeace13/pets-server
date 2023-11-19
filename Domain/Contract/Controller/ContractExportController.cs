using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Contract.Service;
using PetsServer.Organization.Service;

namespace PetsServer.Domain.Contract.Controller;

[Route("сontractexport")]
[ApiController]
public class ContractExportController : ControllerBase
{

    // Маппер для данных
    private readonly IMapper _mapper;

    private ContractService _service;

    public ContractExportController(IMapper mapper)
    {
        _service = new ContractService();
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(string? filters)
    {
        var fileName = $"Контракты.xlsx";
        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var file = _service.ExportToExcel(filters, _mapper);
        return File(file, mimeType, fileName);
    }
}

