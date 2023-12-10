using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Plan.Model;
using PetsServer.Infrastructure;

namespace PetsServer.Domain.Plan.Repository;

public class PlanRepository : BaseRepository<PlanModel>
{
    public PlanRepository() : base()
    {
    }

    public override PlanModel? Get(int id)
    {
        return _context.PlanModels
            .Where(c => c.Id == id)
            .Include(c => c.PlanContent)
            .ThenInclude(pc => pc.Locality)
            .Include(c => c.PlanContent)
            .ThenInclude(pc => pc.Act)
            .Include(p => p.Status)
            .FirstOrDefault();
    }

    public override IQueryable<PlanModel> Get()
    {
        return _context.PlanModels
            .Include(p => p.Status);
    }

    public new int Create(PlanModel plan)
    {
        _context.Add(plan);
        _context.SaveChanges();
        return plan.Id;
    }
}
