using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Contract.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Contract.Repository;

public class ContractContentRepository
{
    private PetsContext _context;

    public ContractContentRepository()
    {
        _context = new PetsContext();
    }

    public ContractContentModel? GetOne(int id)
    {
        return _context.ContractContent
            .Where(c => c.Id == id)
            .Include(cc => cc.Locality)
            .Include(cc => cc.Contract)
            .FirstOrDefault();
    }

    public IEnumerable<ContractContentModel> GetAll(int contractId)
    {
        return _context.ContractContent
            .Where(c => c.ContractId == contractId)
            .Include(cc => cc.Locality)
            .Include(cc => cc.Contract)
            .ToList();
    }

    public void Create(ContractContentModel entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public void Update(ContractContentModel entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(ContractContentModel entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }
}
