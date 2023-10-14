using IS_5.Organization.Model;
using IS_5.Organization.Repository;
using ModelLibrary.Model.Organization;
using ModelLibrary.View;
using PetsServer.Organization.Model;

namespace IS_5.Organization.Service
{
    public class OrganizationService
    {
        private OrganizationRepository _organizationRepository = new OrganizationRepository();

        public void Create(OrganizationEdit view)
        {
            var organization = new OrganizationMapper().FromViewToModel(view);
            _organizationRepository.Create(organization);
        }

        public void Update(int id, OrganizationEdit view)
        {
            var organization = new OrganizationMapper().FromViewToModel(view);
            organization.Id = id;
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

        public PageSettings<OrganizationViewList> GetPage(int? pageQuery, int? pagesQuery, string filter, string? sortField, int? sortType)
        {
            var organizations = _organizationRepository.GetAll();
            var pageSettings = new PageSettings<OrganizationViewList>();
            if (pageQuery.HasValue && pageQuery > 0)
                pageSettings.Page = pageQuery.Value;
            int size = 10;
            if (pagesQuery.HasValue && pagesQuery.Value > 0)
                size = pagesQuery.Value;
            
            organizations = SortOrganizations(FilterOrganizations(organizations, filter), sortField, sortType).ToList();
            pageSettings.Pages = (int)Math.Ceiling((double)organizations.Count / size);
            pageSettings.Items = organizations
                .Skip(size * (pageSettings.Page - 1))
                .Take(size)
                .Select(o => new OrganizationMapper().FromModelToView(o))
                .ToList();
            return pageSettings;
        }

        private IEnumerable<OrganizationModel> SortOrganizations(IEnumerable<OrganizationModel> orgs, string sortField, int? sortType)
        {
            var sort = new SortSettings(sortField, 0);
            if(sortType.HasValue) sort.Direction = sortType.Value;
            if (sort == null) return orgs.OrderBy(org => org.Id);
            IEnumerable<OrganizationModel> result;
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
                        orgs.OrderBy(org => org.Inn)
                        : orgs.OrderByDescending(org => org.Inn);
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

        public IEnumerable<OrganizationModel> FilterOrganizations(List<OrganizationModel> organizations, string filter)
        {
            var filters = new FilterSetting(typeof(OrganizationModel));
            if (filter != null)
            {
                var f = filter.Split(";");
                foreach (string c in f)
                {
                    var a = c.Split(":");
                    filters[a[0]] = Uri.UnescapeDataString(a[1]);
                }
            }
            if (filters == null || filters.Count == 0)
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
                        filteredOrgs = filteredOrgs.Where(o => o.Inn.Contains(value));
                        break;
                    case "KPP":
                        filteredOrgs = organizations.Where(o => o.KPP.Contains(value));
                        break;
                    case "Address":
                        filteredOrgs = organizations.Where(o => o.Address.Contains(value));
                        break;
                    case "TypeOrganization":
                        filteredOrgs = organizations.Where(o => o.TypeOrganization.Name.Contains(value));
                        break;
                    case "LegalType":
                        filteredOrgs = organizations.Where(o => o.LegalType.Name.Contains(value));
                        break;
                    case "Locality":
                        filteredOrgs = organizations.Where(o => o.Locality.Name.Contains(value));
                        break;
                }
            }
            return filteredOrgs;
        }


        

        public void ExportToExcel(string[] columns, List<FilterSetting> filter)
        {
            //ExportDataToExcel.Export<OrganizationViewEdit>(
            //    columns, "Организации", FilterOrganizations(_organizationRepository.GetAll(), filter)
            //    .Select(o => new OrganizationMapper().FromModelToView(o))
            //    .ToList()
            //    );
        }
    }
}
