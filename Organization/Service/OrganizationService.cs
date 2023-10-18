using IS_5.Organization.Repository;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer;
using PetsServer.Authorization.Model;
using PetsServer.Organization.Model;

namespace PetsServer.Organization.Service
{
    public class OrganizationService
    {
        private OrganizationRepository _organizationRepository = new OrganizationRepository();

        public void Create(OrganizationModel organization)
        {
            _organizationRepository.Create(organization);
        }

        public void Update(OrganizationModel organization)
        {
            _organizationRepository.Update(organization);
        }

        public void Delete(int id)
        {
            _organizationRepository.Delete(id);
        }

        public LegalTypeModel GetLegalType(int id)
        {
            return _organizationRepository.GetLegalType(id);
        }

        public List<TypeOrganizationModel> GetTypesOrganization()
        {
            return _organizationRepository.GetTypesOrganization();
        }

        internal List<LegalTypeModel> GetLegalTypes()
        {
            return _organizationRepository.GetLegalTypes();
        }

        public OrganizationModel GetOne(int id)
        {
            return _organizationRepository.GetOne(id);
        }

        public PageSettings<OrganizationModel> GetPage(int? pageQuery, int? limitQuery, string filter, string? sortField, int? sortType, UserModel user)
        {
            
            // создаем страницу с настройками
            var pageSettings = new PageSettings<OrganizationModel>();

            // проверяем, что передано значение для номера страницы
            if (pageQuery.HasValue && pageQuery > 0)
                pageSettings.Page = pageQuery.Value;
            
            // проверяем, что есть значение для объема страницы
            if (limitQuery.HasValue && limitQuery.Value > 0)
                pageSettings.Limit = limitQuery.Value;

            // берем организации по этим правилам
            var organizations = _organizationRepository.GetAll();

            if (user.Privilege.Organizations.Item1 == Restrictions.Organizations)
                organizations = organizations
                    .Where(org => org.Id == user.Organization.Id);

            else if (user.Privilege.Organizations.Item1 == Restrictions.Locality)
                organizations = organizations
                    .Where(org => org.Locality.Id == user.Locality.Id);

            if (user.Privilege.Organizations.Item3 != null)
                organizations = organizations.Where(o => user.Privilege.Organizations.Item3.Contains(o.TypeOrganization.Id));

            // Фильтрация
            organizations = FilterOrganizations(organizations, filter);

            // Сортировка
            organizations = SortOrganizations(organizations, sortField, sortType);

            // Количество страниц всего
            pageSettings.Pages = (int)Math.Ceiling((double)organizations.Count() / pageSettings.Limit);

            // Добавляем элементы в страницу с необходимым количеством
            pageSettings.Items = organizations
                .Skip(pageSettings.Limit * (pageSettings.Page - 1))
                .Take(pageSettings.Limit)
                .ToList();

            return pageSettings;
        }

        private IEnumerable<OrganizationModel> SortOrganizations(IEnumerable<OrganizationModel> orgs, string sortField, int? sortType)
        {
            var sort = new SortSettings(sortField, 0);

            if(sortType.HasValue) sort.Direction = sortType.Value;
            
            IEnumerable<OrganizationModel> result;

            // Сортировка в зависимости от колонки
            switch (sort.Column)
            {
                case "Id":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.Id)
                        : orgs.OrderByDescending(org => org.Id);
                case "NameOrganization":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.NameOrganization)
                        : orgs.OrderByDescending(org => org.NameOrganization);
                case "INN":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.INN)
                        : orgs.OrderByDescending(org => org.INN);
                case "KPP":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.KPP)
                        : orgs.OrderByDescending(org => org.KPP);
                case "Address":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.Address)
                        : orgs.OrderByDescending(org => org.Address);
                case "TypeOrganization":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.TypeOrganization.Name)
                        : orgs.OrderByDescending(org => org.TypeOrganization.Name);
                case "LegalType":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.LegalType.Name)
                        : orgs.OrderByDescending(org => org.LegalType.Name);
                case "Locality":
                    return result = sort.Direction == 0 ?
                        orgs.OrderBy(org => org.Locality.Name)
                        : orgs.OrderByDescending(org => org.Locality.Name);
                default:
                    return orgs.OrderBy(org => org.Id);
            }
        }

        public IEnumerable<OrganizationModel> FilterOrganizations(IEnumerable<OrganizationModel> organizations, string filtersQuery)
        {
            var filters = new FilterSetting(typeof(OrganizationModel));
            if (!String.IsNullOrEmpty(filtersQuery))
            {
                var filtersKeyValue = filtersQuery.Split(";");
                foreach (string filter in filtersKeyValue)
                {
                    var ketValue = filter.Split(":");
                    filters[ketValue[0]] = Uri.UnescapeDataString(ketValue[1]);
                }
            }
            if (filters.CountEmptyFileds == 0)
                return organizations;

            IEnumerable<OrganizationModel> filteredOrgs = organizations;
            foreach (var column in filters.Columns)
            {
                var value = filters[column];
                if (value == null || value == "") continue;
                switch (column)
                {
                    case "NameOrganization":
                        filteredOrgs = filteredOrgs.Where(o => o.NameOrganization.Contains(value));
                        break;
                    case "INN":
                        filteredOrgs = filteredOrgs.Where(o => o.INN.Contains(value));
                        break;
                    case "KPP":
                        filteredOrgs = filteredOrgs.Where(o => o.KPP.Contains(value));
                        break;
                    case "Address":
                        filteredOrgs = filteredOrgs.Where(o => o.Address.Contains(value));
                        break;
                    case "TypeOrganization":
                        filteredOrgs = filteredOrgs.Where(o => o.TypeOrganization.Name.Contains(value));
                        break;
                    case "LegalType":
                        filteredOrgs = filteredOrgs.Where(o => o.LegalType.Name.Contains(value));
                        break;
                    case "Locality":
                        filteredOrgs = filteredOrgs.Where(o => o.Locality.Name.Contains(value));
                        break;
                }
            }
            return filteredOrgs;
        }

        public byte[] ExportToExcel(string filters)
        {
            return ExportDataToExcel.Export(
                "Организации", FilterOrganizations(_organizationRepository.GetAll(), filters)
                // TODO Надо что-то придумать! Это дичь
                .Select(o => new OrganizationViewList()
                {
                    Id = o.Id,
                    NameOrganization = o.NameOrganization,
                    INN = o.INN,
                    KPP = o.KPP,
                    Address = o.Address,
                    TypeOrganization = o.TypeOrganization.Name,
                    LegalType = o.LegalType.Name,
                    Locality = o.Locality.Name
                })
                .ToList()
                );
        }
    }
}
