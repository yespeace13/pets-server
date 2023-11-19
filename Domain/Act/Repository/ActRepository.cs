using PetsServer.Domain.Act.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Act.Repository;

public class ActRepository
{
    private PetsContext _context;

    public ActRepository()
    {
        _context = new PetsContext();
    }

    public void Create(ActModel acts)
    {
        //_context..Add(acts);
        //_context.SaveChanges();
    }

    public void Update(ActModel acts)
    {
        //_context.Contracts.Update(acts);
        //_context.SaveChanges();
    }

    public void Delete(ActModel acts)
    {
        //_context.Contracts.Remove(acts);
        //_context.SaveChanges();
    }
}