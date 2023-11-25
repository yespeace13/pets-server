using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Contract.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Contract.Repository;

public class ContractRepository
{
    private PetsContext _context;

    public ContractRepository()
    {
        _context = new PetsContext();
    }

    public PlanModel? GetOne(int id)
    {
        return _context.Plan
            .Include(c => c.Client.TypeOrganization)
            .Include(c => c.Client.LegalType)
            .Include(c => c.Client.Locality)
            .Include(c => c.Executor.TypeOrganization)
            .Include(c => c.Executor.LegalType)
            .Include(c => c.Executor.Locality)
            .Include(c => c.ContractContent)
            .ThenInclude(c => c.Locality)
            .FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<PlanModel> GetAll()
    {
        return _context.Plan
            .Include(c => c.Client)
            .Include(c => c.Executor)
            .ToList();
    }

    public void Create(PlanModel contracts)
    {
        _context.Add(contracts);
        _context.SaveChanges();
    }

    public void Update(PlanModel contracts)
    {
        _context.Update(contracts);
        _context.SaveChanges();
    }

    public void Delete(PlanModel contracts)
    {
        _context.Remove(contracts);
        _context.SaveChanges();
    }
}
