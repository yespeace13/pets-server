using AutoMapper;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Authorization.Model;
using PetsServer.Organization.Model;
using PetsServer.Organization.Repository;

namespace PetsServer.Organization.Service;

public class OrganizationService
{
    private OrganizationRepository _repository = new OrganizationRepository();

    public void Create(OrganizationModel organization)
    {
        _repository.Create(organization);
    }

    public void Update(OrganizationModel organization)
    {
        var oldOrg = GetOne(organization.Id);
        oldOrg.NameOrganization = organization.NameOrganization;
        oldOrg.INN = organization.INN;
        oldOrg.KPP = organization.KPP;
        oldOrg.Address = organization.Address;
        oldOrg.TypeOrganizationId = organization.TypeOrganizationId;
        oldOrg.LegalTypeId = organization.LegalTypeId;
        oldOrg.LocalityId = organization.LocalityId;
        _repository.Update(oldOrg);
    }

    public void Delete(int id)
    {
        var organization = GetOne(id);
        _repository.Delete(organization);
    }

    public LegalTypeModel? GetLegalType(int id) => _repository.GetLegalType(id);

    public TypeOrganizationModel? GetTypeOrganization(int id) => _repository.GetTypeOrganization(id);

    public List<TypeOrganizationModel> GetTypesOrganization() => _repository.GetTypesOrganization();

    public List<LegalTypeModel> GetLegalTypes() => _repository.GetLegalTypes();

    public OrganizationModel? GetOne(int id) => _repository.GetOne(id);

    public PageSettings<OrganizationViewList> GetPage(int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<OrganizationViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        // берем организации по этим правилам
        var organizations = _repository.GetAll();

        var userRestiction = user.Role.Possibilities.Where(p => p.Entity == Entities.Organization && p.Possibility == Possibilities.Read).First().Restriction;

        if (userRestiction == Restrictions.Organization)
            organizations = organizations.Where(org => org.Id == user.Organization.Id);

        else if (userRestiction == Restrictions.Locality)
            organizations = organizations.Where(org => org.Locality.Id == user.Locality.Id);

        // TODO пока оставил так, хз что делать, для какой-то роли надо было выводить только какого-то типа организации
        //if (user.Privilege.Organization.TypeOrganizations != null)
        //    organizations = organizations.Where(o => user.Privilege.Organization.TypeOrganizations.Contains(o.TypeOrganization.Id));

        var organizationsView = mapper.Map<IEnumerable<OrganizationViewList>>(organizations);
        // Фильтрация
        organizationsView = new FilterObjects<OrganizationViewList>().Filter(organizationsView, filter);

        // Сортировка
        organizationsView = new SorterObjects<OrganizationViewList>().SortField(organizationsView, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)organizationsView.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = organizationsView
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<OrganizationViewList> organizations = mapper.Map<List<OrganizationViewList>>(_repository.GetAll());
        organizations = new FilterObjects<OrganizationViewList>().Filter(organizations, filters);
        return ExportDataToExcel.Export(
            "Организации", organizations.ToList());
    }
}

