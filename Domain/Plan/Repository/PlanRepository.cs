using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Plan.Model;
using PetsServer.Infrastructure;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Plan.Repository;

public class PlanRepository : BaseRepository<PlanModel>
{
    public PlanRepository() : base()
    {
    }

    public PlanModel? GetOne(int id)
    {
        return _context.PlanModels
            .Include(p => p.PlanContent)
            .ThenInclude(cc => cc.Locality)
            .Include(p => p.PlanContent)
            .ThenInclude(cc => cc.Act)
            .FirstOrDefault(p => p.Id == id);

    }

    public override PlanModel? Get(int id)
    {
        return _context.PlanModels
            .Where(c => c.Id == id)
            .Include(c => c.PlanContent)
            .FirstOrDefault();
    }

    public override IQueryable<PlanModel> Get()
    {
        return _context.PlanModels;
    } 
}
