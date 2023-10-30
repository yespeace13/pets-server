using AutoMapper;
using ModelLibrary.Model.Contract;
using ModelLibrary.View;
using PetsServer.Authorization.Model;
using PetsServer.Contract.Model;
using PetsServer.Contract.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Contract.Service;

public class ContractService
{
    private ContractRepository _repository = new ContractRepository();

    public void Create(ContractModel model)
    {
        _repository.Create(model);
    }

    public void Update(ContractModel model)
    {
        var oldModel = GetOne(model.Id);
        oldModel.Number = model.Number;
        oldModel.DateOfConclusion = model.DateOfConclusion;
        oldModel.DateValid = model.DateValid;
        oldModel.ClientId = model.ClientId;
        oldModel.ExecutorId = model.ExecutorId;
        _repository.Update(oldModel);
    }

    public void Delete(int id)
    {
        var organization = GetOne(id);
        _repository.Delete(organization);
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

        // берем организации по этим правилам
        var contracts = _repository.GetAll();

        var userRestiction = user.Role.Possibilities.Where(p => p.Entity == Entities.Organization && p.Possibility == Possibilities.Read).First().Restriction;

        if (userRestiction == Restrictions.Organization)
            contracts = contracts.Where(c => c.ExecutorId == user.Organization.Id);

        else if (userRestiction == Restrictions.Locality)
            contracts = contracts.Where(c => c.ContractContents.Where(cc => cc.LocalityId == user.Locality.Id).Any());

        IEnumerable<ContractViewList> contractsView = mapper.Map<List<ContractViewList>>(contracts);
        // Фильтрация
        contractsView = new FilterObjects<ContractViewList>().Filter(contractsView, filter);

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

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<ContractViewList> organizations = mapper.Map<List<ContractViewList>>(_repository.GetAll());
        organizations = new FilterObjects<ContractViewList>().Filter(organizations, filters);
        return ExportDataToExcel.Export(
            "Организации", organizations.ToList());
    }
}

