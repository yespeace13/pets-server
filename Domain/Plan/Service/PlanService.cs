using AutoMapper;
using ModelLibrary.Model.Plan;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Plan.Model;
using PetsServer.Domain.Plan.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Plan.Service;

public class PlanService
{
    private readonly PlanRepository _repository = new();

    public int Create(PlanModel model)
    {
        return _repository.Create(model);
    }

    public void Update(PlanModel model)
    {
        var oldModel = GetOne(model.Id);
        oldModel.Month = model.Month;
        oldModel.Year = model.Year;
        oldModel.PlanContent = model.PlanContent;
        _repository.Update(oldModel);
    }

    public void Delete(int id)
    {
        var model = GetOne(id);
        if(model != null)
            _repository.Delete(model);
    }

    public PlanModel? GetOne(int id) => _repository.Get(id);

    public PageSettings<PlanViewList> GetPage(
        int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<PlanViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        // берем по этим правилам
        var plans = _repository.Get();

        var userRestiction = user.Role.Possibilities.Where(p => p.Entity == Entities.Schedule && p.Possibility == Possibilities.Read).First().Restriction;

        //if (userRestiction == Restrictions.Organization)
        //    plans = plans.Where(c => c. == user.Organization.Id);

        if (userRestiction == Restrictions.Locality)
            plans = plans.Where(p => p.PlanContent.Where(pp => pp.LocalityId == user.Locality.Id).Any());

        IEnumerable<PlanViewList> plansView = mapper.Map<List<PlanViewList>>(plans);
        // Фильтрация
        plansView = new FilterObjects<PlanViewList>().Filter(plansView, filter);

        // Сортировка
        plansView = new SorterObjects<PlanViewList>().SortField(plansView, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)plansView.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = plansView
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<PlanViewList> plans = mapper.Map<List<PlanViewList>>(_repository.Get());
        plans = new FilterObjects<PlanViewList>().Filter(plans, filters);
        return ExportDataToExcel.Export(
            "План-графики", plans.ToList());
    }
}

