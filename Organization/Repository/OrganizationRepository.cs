using IS_5.Organization.Model;
using Microsoft.EntityFrameworkCore;
using PetsServer.Context;
using PetsServer.Organization.Model;

namespace IS_5.Organization.Repository
{
    /**
     * Пока выглядит немного убого, скорее всего буду переделывать
    */
    public class OrganizationRepository
    {

        public OrganizationModel GetOne(int id)
        {
            using var context = new PetsContext();
            return context.Organizations.Where(o => o.Id == id)
                .Include(o => o.TypeOrganization)
                .Include(o => o.LegalType)
                .Include(o => o.Locality)
                .First();
        }

        public TypeOrganizationModel GetTypeOrganization(string typeOrganization)
        {
            using var context = new PetsContext();
            return context.TypeOrganizations.Where(t => t.Name == typeOrganization)
                .First();
        }

        public TypeOrganizationModel GetTypeOrganization(int id)
        {
            using var context = new PetsContext();
            return context.TypeOrganizations.Where(t => t.Id == id)
                .First();
        }
        public LegalTypeModel GetLegalType(string legalType)
        {
            using var context = new PetsContext();
            return context.LegalTypes.Where(l => l.Name == legalType)
                .First();
        }
        public LegalTypeModel GetLegalType(int id)
        {
            using var context = new PetsContext();
            return context.LegalTypes.Where(l => l.Id == id)
                .First();
        }
        public List<TypeOrganizationModel> GetTypesOrganization()
        {
            using var context = new PetsContext();
            return context.TypeOrganizations.ToList();
        }

        public List<LegalTypeModel> GetLegalTypes()
        {
            using var context = new PetsContext();
            return context.LegalTypes.ToList();
        }
        public List<OrganizationModel> GetAll()
        {
            using var context = new PetsContext();
            return context.Organizations.Include(o => o.TypeOrganization)
                .Include(o => o.LegalType)
                .Include(o => o.Locality)
                .ToList();
        }

        public void Create(OrganizationModel organization)
        {
            using var context = new PetsContext();
            context.Add(organization);
            context.SaveChanges();
        }

        public void Update(OrganizationModel organization)
        {
            using var context = new PetsContext();
            var oldOrg = context.Organizations.Where(o => o.Id == organization.Id)
                .First();
            oldOrg.NameOrganization = organization.NameOrganization;
            oldOrg.Inn = organization.Inn;
            oldOrg.KPP = organization.KPP;
            oldOrg.Address = organization.Address;
            oldOrg.TypeOrganizationId = organization.TypeOrganizationId;
            oldOrg.LegalTypeId = organization.LegalTypeId;
            oldOrg.LocalityId = organization.LocalityId;
            context.Organizations.Update(oldOrg);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            using var context = new PetsContext();
            var organization = context.Organizations.Where(o => o.Id == id)
                .First();
            context.Organizations.Remove(organization);
            context.SaveChanges();
        }


    }
}
