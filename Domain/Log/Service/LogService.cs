using AutoMapper;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Log.Repository;
using PetsServer.Domain.Organization.Model;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Log.Service;

public class LogService
{
    private LogRepository _repository = new LogRepository();

    public void Delete(int id)
    {
        var organization = GetOne(id);
        _repository.Delete(organization);
    }

    public LogModel? GetOne(int id) => _repository.Get(id);

    //public PageSettings<LogModel> GetPage(int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    //{
    //    // создаем страницу с настройками
    //    var pageSettings = new PageSettings<OrganizationViewList>();

    //    // проверяем, что передано значение для номера страницы
    //    if (pageQuery.HasValue && pageQuery > 0)
    //        pageSettings.Page = pageQuery.Value;

    //    // проверяем, что есть значение для объема страницы
    //    if (limitQuery.HasValue && limitQuery.Value > 0)
    //        pageSettings.Limit = limitQuery.Value;

    //    // берем организации по этим правилам
    //    var organizations = _repository.Get();

    //    var organizationsView = mapper.Map<IEnumerable<OrganizationViewList>>(organizations);
    //    // Фильтрация
    //    organizationsView = new FilterObjects<OrganizationViewList>().Filter(organizationsView, filter);

    //    // Сортировка
    //    organizationsView = new SorterObjects<OrganizationViewList>().SortField(organizationsView, sortField, sortType);

    //    // Количество страниц всего
    //    pageSettings.Pages = (int)Math.Ceiling((double)organizationsView.Count() / pageSettings.Limit);

    //    // Добавляем элементы в страницу с необходимым количеством
    //    pageSettings.Items = organizationsView
    //        .Skip(pageSettings.Limit * (pageSettings.Page - 1))
    //        .Take(pageSettings.Limit);

    //    return pageSettings;
    //}

    //public byte[] ExportToExcel(string filters, IMapper mapper)
    //{
    //    IEnumerable<LogModel> organizations = mapper.Map<List<OrganizationViewList>>(_repository.Get());
    //    organizations = new FilterObjects<OrganizationViewList>().Filter(organizations, filters);
    //    return ExportDataToExcel.Export(
    //        "Организации", organizations.ToList());
    //}
}

