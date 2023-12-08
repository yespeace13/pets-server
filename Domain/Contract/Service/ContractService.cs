using AutoMapper;
using ModelLibrary.Model.Contract;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Contract.Service;

public class ContractService
{
    private ContractRepository _repository = new ContractRepository();

    public int Create(ContractModel model)
    {
        return _repository.Create(model);
    }

    public void Update(ContractModel model)
    {
        var oldModel = GetOne(model.Id);
        if (oldModel == null) return;
        oldModel.Number = model.Number;
        oldModel.DateOfConclusion = model.DateOfConclusion;
        oldModel.DateValid = model.DateValid;
        oldModel.ClientId = model.ClientId;
        oldModel.ExecutorId = model.ExecutorId;
        oldModel.ContractContent = model.ContractContent;
        _repository.Update(oldModel);
    }

    public void Delete(int id)
    {
        var model = GetOne(id);
        _repository.Delete(model);
    }

    public ContractModel? GetOne(int id) => _repository.GetOne(id);

    public PageSettings<ContractViewList> GetPage(
        int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<ContractViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        var contracts = Get(user);


        // Фильтрация
        //contractsView = new FilterObjects<ContractViewList>().Filter(contractsView, filter);
        contracts = Filter(contracts, filter);
        // маппим для фильтрации и сортировки
        IEnumerable<ContractViewList> contractsView = mapper.Map<IEnumerable<ContractViewList>>(contracts);
        // Сортировка
        contractsView = new SorterObjects<ContractViewList>().SortField(contractsView, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)contractsView.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = contractsView
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    // TODO другие сервисы надо будет так же сделать
    public IQueryable<ContractModel> Get(UserModel user)
    {
        var contracts = _repository.Get();

        var userRestiction = user.Role.Possibilities.Where(p => p.Entity == Entities.Contract && p.Possibility == Possibilities.Read)
            .First().Restriction;

        if (userRestiction == Restrictions.Organization)
            contracts = contracts.Where(c => c.ExecutorId == user.Organization.Id);

        else if (userRestiction == Restrictions.Locality)
            contracts = contracts.Where(c => c.ContractContent.Where(cc => cc.LocalityId == user.Locality.Id).Any());

        return contracts;
    }

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<ContractViewList> organizations = mapper.Map<List<ContractViewList>>(_repository.GetAll());
        organizations = new FilterObjects<ContractViewList>().Filter(organizations, filters);
        return ExportDataToExcel.Export(
            "Организации", organizations.ToList());
    }


    /// <summary>
    /// Фильтрация для контракта
    /// </summary>
    /// <param name="models">Контракты</param>
    /// <param name="filtersQuery">Фильтр</param>
    /// <returns></returns>
    private IQueryable<ContractModel> Filter(IQueryable<ContractModel> models, string filtersQuery)
    {
        if (string.IsNullOrEmpty(filtersQuery)) return models;

        var filters = new FilterSetting(typeof(ContractViewList));

        // TODO потом отдельна куда-нибудь вывести
        var filtersKeyValue = filtersQuery.Split(";");
        foreach (string filter in filtersKeyValue)
        {
            var ketValue = filter.Split(":");
            filters[ketValue[0]] = Uri.UnescapeDataString(ketValue[1]);
        }

        if (filters.CountEmptyFileds == 0)
            return models;

        foreach (var filter in filters.Columns)
        {
            switch (filter)
            {
                case "Id":
                    models = models.Where(m => m.Id.ToString().Contains(filters[filter]));
                    break;
                case "Number":
                    models = models.Where(m => m.Number.Contains(filters[filter]));
                    break;
                case "DateOfConclusion":
                    var periodDate1 = filters[filter].Split(' ');
                    if (DateOnly.TryParse(periodDate1[0], out var startDate1) && DateOnly.TryParse(periodDate1[1], out var endDate1))
                    {
                        models = models.Where(m => m.DateOfConclusion >= startDate1 && m.DateOfConclusion <= endDate1);
                    }
                    break;
                case "DateValid":
                    var periodDate2 = filters[filter].Split(' ');
                    if (DateOnly.TryParse(periodDate2[0], out var startDate2) && DateOnly.TryParse(periodDate2[1], out var endDate2))
                    {
                        models = models.Where(m => m.DateValid >= startDate2 && m.DateValid <= endDate2);
                    }
                    break;
                // не работает, так как окно фильтра не применяется к дате окончания
                default:
                    break;
            }
        }
        return models;
    }
}

