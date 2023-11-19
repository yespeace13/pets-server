using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Organization.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Organization.Repository;

public class OrganizationRepository
{
    private PetsContext _context;

    public OrganizationRepository()
    {
        _context = new PetsContext();
    }

    public OrganizationModel? GetOne(int id)
    {
        return _context.Organizations
            .Include(o => o.TypeOrganization)
            .Include(o => o.LegalType)
            .Include(o => o.Locality)
            .FirstOrDefault(o => o.Id == id);
    }

    public TypeOrganizationModel? GetTypeOrganization(int id) => _context.TypeOrganizations.Find(id);

    public LegalTypeModel? GetLegalType(int id) => _context.LegalTypes.Find(id);

    public List<TypeOrganizationModel> GetTypesOrganization() => _context.TypeOrganizations.ToList();

    public List<LegalTypeModel> GetLegalTypes() => _context.LegalTypes.ToList();

    public IEnumerable<OrganizationModel> GetAll()
    {
        return _context.Organizations.Include(o => o.TypeOrganization)
            .Include(o => o.LegalType)
            .Include(o => o.Locality)
            .ToList();
    }

    public void Create(OrganizationModel organization)
    {
        _context.Add(organization);
        _context.SaveChanges();
    }

    public void Update(OrganizationModel organization)
    {
        _context.Organizations.Update(organization);
        _context.SaveChanges();
    }

    public void Delete(OrganizationModel organization)
    {
        _context.Organizations.Remove(organization);
        _context.SaveChanges();
    }
}
