using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Contract.Repository;
using PetsServer.Domain.Organization.Repository;
using PetsServer.Infrastructure.Context;
using System.Net;

namespace PetsServer.Domain.Act.Repository;

public class ActRepository
{
    private PetsContext _context;

    public ActRepository() => _context = new PetsContext();

    public ActModel? GetOne(int id)
    {
        var act = _context.Acts
            .Include(a => a.Animal)
            .Include(a => a.Locality)
            .Include(a => a.Executor)
            .Include(a => a.Contract)
            .FirstOrDefault(a => a.Id == id);
        return act;
    }

    public IEnumerable<ActModel> GetAll()
    {
        return _context.Acts
            .Include(c => c.Executor)
            .Include(c => c.Locality)
            .Include(c => c.Contract)
            .ToList();
    }

    public void Create(ActModel act)
    {
        _context.Add(act);
        _context.SaveChanges();
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
