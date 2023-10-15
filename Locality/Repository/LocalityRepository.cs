using IS_5;
using PetsServer.Context;
using PetsServer.Locality.Model;

namespace PetsServer.Locality.Repository
{
    internal class LocalityRepository
    {
        public List<LocalityModel> GetLocalitys()
        {
            using(var context = new PetsContext())
            {
                return context.Localities.ToList();
            }
        }

        public LocalityModel GetOne(int id)
        {
            using (var context = new PetsContext())
            {
                return context.Localities.First(l => l.Id == id);
            }
        }

        public LocalityModel GetOne(string name)
        {
            using (var context = new PetsContext())
            {
                return context.Localities.First(l => l.Name == name);
            }
        }
    }
}
