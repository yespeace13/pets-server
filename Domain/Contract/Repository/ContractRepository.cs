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

    public ContractModel? GetOne(int id)
    {
        return _context.Contracts
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

    public IEnumerable<ContractModel> GetAll()
    {
        return _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.Executor)
            .ToList();
    }

    public IQueryable<ContractModel> Get()
    {
        return _context.Contracts
            .Include(c => c.Client.TypeOrganization)
            .Include(c => c.Client.LegalType)
            .Include(c => c.Client.Locality)
            .Include(c => c.Executor.TypeOrganization)
            .Include(c => c.Executor.LegalType)
            .Include(c => c.Executor.Locality)
            .Include(c => c.ContractContent)
            .ThenInclude(c => c.Locality);
    }

    public void Create(ContractModel contracts)
    {
        _context.Add(contracts);
        _context.SaveChanges();
    }

    public void Update(ContractModel contracts)
    {
        _context.Update(contracts);
        _context.SaveChanges();
    }

    public void Delete(ContractModel contracts)
    {
        _context.Remove(contracts);
        _context.SaveChanges();
    }
}
