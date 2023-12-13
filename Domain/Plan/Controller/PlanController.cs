using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsServer.Auth.Authentication;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Auth.Authorization.Service;
using PetsServer.Domain.Plan.Service;
using ModelLibrary.Model.Plan;
using PetsServer.Domain.Plan.Model;
using PetsServer.Domain.Log.Service;

namespace PetsServer.Domain.Plan.Controller;

[ApiController]
[Route("plans")]
[Authorize]
public class PlanController(IMapper mapper) : ControllerBase
{
    // Сервис
    private readonly PlanService _service = new();
    // Для привилегий и доступа
    private readonly AuthenticationService _authenticationService = new();
    // Маппер для данных
    private readonly IMapper _mapper = mapper;
    private readonly LoggerFacade _logger = new(new DbLogger(), new TxtLogger());

    [HttpGet(Name = "GetPlans")]
    public IActionResult GetPage(
        int? page,
        int? pages,
        string? filter,
        string? sortField,
        int? sortType)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Schedule, user))
            return Problem(null, null, 403, "У вас нет привилегий");

        var pageView = _service.GetPage(page, pages, filter, sortField, sortType, user, _mapper);

        return Ok(pageView);
    }

    [HttpGet("{id}", Name = "GetPlan")]
    public IActionResult GetOne(int id)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Read, Entities.Schedule, user))
            return Problem(null, null, 403, "У вас нет привилегий");
        var entity = _service.GetOne(id);
        var view = _mapper.Map<PlanViewOne>(entity);
        return Ok(view);
    }

    [HttpPost(Name = "CreatePlan")]
    public IActionResult Create([FromBody] PlanEdit view)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Insert, Entities.Schedule, user))
            return Problem(null, null, 403, "У вас нет привилегий");
        var entity = _mapper.Map<PlanEdit, PlanModel>(view);
        var id = _service.Create(entity);
        _logger.Log(user, Entities.Schedule, Possibilities.Insert, typeof(PlanModel), id);
        return Ok();
    }

    [HttpPut("{id}", Name = "UpdatePlan")]
    public IActionResult Update(int id, PlanEdit view)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Update, Entities.Schedule, user))
            return Problem(null, null, 403, "У вас нет привилегий");

        var entity = _mapper.Map<PlanEdit, PlanModel>(view);
        entity.Id = id;
        _service.Update(entity);
        _logger.Log(user, Entities.Schedule, Possibilities.Update, typeof(PlanModel), id);
        return Ok();
    }

    [HttpDelete("{id}", Name = "DeletePlan")]
    public ActionResult Delete(int id)
    {
        var user = _authenticationService.GetUser(User.Identity.Name);

        if (!AuthorizationService.IsPossible(Possibilities.Delete, Entities.Schedule, user))
            return Problem(null, null, 403, "У вас нет привилегий");

        _service.Delete(id);
        _logger.Log(user, Entities.Schedule, Possibilities.Delete, typeof(PlanModel), id);
        return Ok();
    }
}
