using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Act.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Act.Repository;

public class ActRepository
{
    private readonly PetsContext _context = new();

    public ActModel? Get(int id)
    {
        var act = _context.Acts
            .Include(a => a.Animal)
            .Include(a => a.Locality)
            .Include(a => a.Executor)
            .Include(a => a.Contract)
            .FirstOrDefault(a => a.Id == id);
        return act;
    }

    public IQueryable<ActModel> Get()
    {
        return _context.Acts
            .Include(c => c.Executor)
            .Include(c => c.Locality)
            .Include(c => c.Contract);
    }

    public int Create(ActModel act)
    {
        _context.Add(act);
        _context.SaveChanges();
        return act.Id;
    }

    public void Update(ActModel act)
    {
        _context.Update(act);
        _context.SaveChanges();
    }

    public void Delete(ActModel act)
    {
        _context.Remove(act);
        _context.SaveChanges();
    }
}
