using PetsServer.Context;
using PetsServer.Locality.Model;

namespace PetsServer.Locality.Repository
{
    public class LocalityRepository
    {
        private PetsContext _context;

        public LocalityRepository() => _context = new PetsContext();

        public List<LocalityModel> GetLocalitys() => _context.Localities.ToList();

        public LocalityModel? GetOne(int id) => _context.Localities.Find(id);

    }
}
