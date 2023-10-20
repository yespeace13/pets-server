using Microsoft.EntityFrameworkCore;
using PetsServer.Contract.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Contract.Repository;

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
            .Include(c => c.Client)
            .Include(c => c.Executor)
            .FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<ContractModel> GetAll()
    {
        return _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.Executor)
            .ToList();
    }

    public void Create(ContractModel contracts)
    {
        _context.Contracts.Add(contracts);
        _context.SaveChanges();
    }

    public void Update(ContractModel contracts)
    {
        _context.Contracts.Update(contracts);
        _context.SaveChanges();
    }

    public void Delete(ContractModel contracts)
    {
        _context.Contracts.Remove(contracts);
        _context.SaveChanges();
    }
}
