using Microsoft.EntityFrameworkCore;
using PetsServer.Base.Repository;
using PetsServer.Domain.Organization.Model;

namespace PetsServer.Domain.Organization.Repository;

public class OrganizationRepository : BaseRepository<OrganizationModel>
{
    public TypeOrganizationModel? GetTypeOrganization(int id) => _context.TypeOrganizations.Find(id);

    public LegalTypeModel? GetLegalType(int id) => _context.LegalTypes.Find(id);

    public List<TypeOrganizationModel> GetTypesOrganization() => _context.TypeOrganizations.ToList();

    public List<LegalTypeModel> GetLegalTypes() => _context.LegalTypes.ToList();

    public new int Create(OrganizationModel organization)
    {
        _context.Add(organization);
        _context.SaveChanges();
        return organization.Id;
    }

    public override OrganizationModel? Get(int id)
    {
        return _context.Organizations
            .Include(o => o.TypeOrganization)
            .Include(o => o.LegalType)
            .Include(o => o.Locality)
            .FirstOrDefault(o => o.Id == id);
    }

    public override IQueryable<OrganizationModel> Get()
    {
        return _context.Organizations.Include(o => o.TypeOrganization)
            .Include(o => o.LegalType)
            .Include(o => o.Locality);
    }
}
