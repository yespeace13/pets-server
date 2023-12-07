using AutoMapper;
using ModelLibrary.Model.Act;
using ModelLibrary.Model.Contract;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Act.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Act.Service;

public class ActService
{
    private ActRepository _repository = new ActRepository();

    public int Create(ActModel model)
    {
        return _repository.Create(model);
    }

    public void Update(ActModel model)
    {
        var oldModel = GetOne(model.Id);
        oldModel.DateOfCapture = model.DateOfCapture;
        oldModel.ExecutorId = model.ExecutorId;
        oldModel.LocalityId = model.LocalityId;
        oldModel.ContractId = model.ContractId;
        oldModel.Animal = model.Animal;
        _repository.Update(oldModel);
    }

    public void Delete(int id)
    {
        var organization = GetOne(id);
        _repository.Delete(organization);
    }

    public ActModel? GetOne(int id) => _repository.GetOne(id);

    public PageSettings<ActViewList> GetPage(
        int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<ActViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        // берем организации по этим правилам
        var acts = _repository.GetAll();

        var userRestiction = user.Role.Possibilities.Where(p => p.Entity == Entities.Organization && p.Possibility == Possibilities.Read).First().Restriction;

        if (userRestiction == Restrictions.Organization)
            acts = acts.Where(a => a.ExecutorId == user.Organization.Id);

        else if (userRestiction == Restrictions.Locality)
            acts = acts.Where(a => a.LocalityId == user.Locality.Id);

        IEnumerable<ActViewList> actsView = mapper.Map<IEnumerable<ActModel>, IEnumerable<ActViewList>>(acts);
        // Фильтрация
        actsView = new FilterObjects<ActViewList>().Filter(actsView, filter);

        // Сортировка
        actsView = new SorterObjects<ActViewList>().SortField(actsView, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)actsView.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = actsView
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<ActViewList> acts = mapper.Map<List<ActViewList>>(_repository.GetAll());
        acts = new FilterObjects<ActViewList>().Filter(acts, filters);
        return ExportDataToExcel.Export(
            "Организации", acts.ToList());
    }
}

