using Microsoft.EntityFrameworkCore;
using PetsServer.Domain.Log.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Log.Repository;

public class LogRepository
{
    private PetsContext _context = new PetsContext();
    public LogModel? Get(int id)
    {
        return _context.Logs
            .Include(l => l.User)
            .ThenInclude(u => u.Organization)
            .FirstOrDefault(l => l.Id == id);
    }
    public IQueryable<LogModel> Get()
    {
        return _context.Logs
            .Include(l => l.User)
            .ThenInclude(u => u.Organization);
    }


    public void Delete(LogModel entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

    public void Create(LogModel entity)
    {
        _context.Logs.Add(entity);
        _context.SaveChanges();
    }
}
