using PetsServer.Domain.Log.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Log.Repository;

public class LogRepository
{
    private PetsContext _context = new PetsContext();
    public LogModel Get(int id)
    {
        return null;
    }
    public IQueryable<LogModel> Get()
    {
        return null;
    }


    public void Delete(LogModel entity)
    {
        //_context.Remove(entity);
        //_context.SaveChanges();
    }
}
