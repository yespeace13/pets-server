using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Log.Controller;

[Route("logexport")]
[ApiController]
public class LogExportController : ControllerBase
{

    // Маппер для данных
    private readonly IMapper _mapper;

    private LogService _service;

    public LogExportController(IMapper mapper)
    {
        _service = new LogService();
        _mapper = mapper;
    }

    //[HttpGet]
    //public IActionResult Get(string? filters)
    //{
    //    var fileName = $"Журнал.xlsx";
    //    var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //    var file = _service.ExportToExcel(filters, _mapper);
    //    return File(file, mimeType, fileName);
    //}
}

