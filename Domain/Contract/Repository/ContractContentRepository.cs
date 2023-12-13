using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Contract.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Contract.Repository;

public class ContractContentRepository
{
    private readonly PetsContext _context = new();

    public ContractContentModel? GetOne(int id)
    {
        return _context.ContractContents
            .Where(c => c.Id == id)
            .Include(cc => cc.Locality)
            .Include(cc => cc.Contract)
            .FirstOrDefault();
    }

    public IEnumerable<ContractContentModel> GetAll()
    {
        return _context.ContractContents
            .Include(cc => cc.Locality)
            .Include(cc => cc.Contract)
            .ToList();
    }
    internal IQueryable<ContractContentModel> GetContractAll(int contractId)
    {
        return _context.ContractContents
             .Where(cc => cc.ContractId == contractId)
             .Include(cc => cc.Locality)
             .Include(cc => cc.Contract);
    }
}
