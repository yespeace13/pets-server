using IS_5.Organization.Model;
using Microsoft.EntityFrameworkCore;
using PetsServer.Context;
using PetsServer.Organization.Model;
using System.Security.Claims;

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
            using (var context = new PetsContext())
            {
                return context.Organizations.Include(o => o.TypeOrganization)
                    .Include(o => o.LegalType)
                    .Include(o => o.Locality)
                    .ToList();
            }
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
