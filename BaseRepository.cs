using PetsServer.Infrastructure.Context;

namespace PetsServer
{
    public abstract class BaseRepository<T>
    {
        protected PetsContext _context;

        public BaseRepository() => _context = new PetsContext();
        
        public abstract T? Get(int id);
        public abstract IQueryable<T> Get();

        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
    }
}
