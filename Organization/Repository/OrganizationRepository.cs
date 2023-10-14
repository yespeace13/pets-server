using IS_5.Organization.Model;
using PetsServer.Organization.Model;

namespace IS_5.Organization.Repository
{
    public class OrganizationRepository
    {
        

        public OrganizationModel GetOne(int id)
        {
            return TestData.OrganizationsModel.First(x => x.Id == id);
        }

        public TypeOrganizationModel GetTypeOrganization(string typeOrganization)
        {
            return TestData.TypeOrganizationsModel.First(t => t.Name == typeOrganization);
        }

        public TypeOrganizationModel GetTypeOrganization(int id)
        {
            return TestData.TypeOrganizationsModel.First(t => t.Id == id);
        }
        public LegalTypeModel GetLegalType(string legalType)
        {
            return TestData.LegalTypesModel.First(t => t.Name == legalType);
        }
        public LegalTypeModel GetLegalType(int id)
        {
            return TestData.LegalTypesModel.First(t => t.Id == id);
        }
        internal List<TypeOrganizationModel> GetTypesOrganization()
        {
            return TestData.TypeOrganizationsModel;
        }

        internal List<LegalTypeModel> GetLegalTypes()
        {
            return TestData.LegalTypesModel;
        }
        public List<OrganizationModel> GetAll()
        {
            IEnumerable<OrganizationModel> orgs = TestData.OrganizationsModel;
            //if (UserSession.User.Privilege.Organizations.Item1 == Restrictions.Organizations)
            //    orgs = TestData.OrganizationsModel
            //        .Where(org => org.NameOrganization == UserSession.User.Organization.NameOrg);
            //else if (UserSession.User.Privilege.Organizations.Item1 == Restrictions.Locality)
            //    orgs = TestData.OrganizationsModel
            //        .Where(org => org.Locality.Name == UserSession.User.Locality.Name);
            //else
            //    orgs = TestData.OrganizationsModel;
            //if (UserSession.User.Privilege.Organizations.Item3 != null)
            //    orgs = orgs.Where(o => UserSession.User.Privilege.Organizations.Item3.Contains(o.TypeOrganization.Id));
            return orgs.ToList();
        }

        internal void Create(OrganizationModel organization)
        {
            var newId = TestData.OrganizationsModel.Max(o => o.Id); 
            organization.Id = ++newId;
            TestData.OrganizationsModel.Add(organization);
        }

        internal void Update(OrganizationModel organization)
        {
            var oldOrg = TestData.OrganizationsModel.First(o => o.Id == organization.Id);
            oldOrg.NameOrganization = organization.NameOrganization;
            oldOrg.TypeOrganization = organization.TypeOrganization;
            oldOrg.LegalType = organization.LegalType;
            oldOrg.Locality = organization.Locality;
        }

        internal void Delete(int id)
        {
            TestData.OrganizationsModel.Remove(TestData.OrganizationsModel.First(o => o.Id == id));
        }

        
    }
}
