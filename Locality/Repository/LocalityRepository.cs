using IS_5;
using PetsServer.Locality.Model;

namespace PetsServer.Locality.Repository
{
    internal class LocalityRepository
    {
        public List<LocalityModel> GetLocalitys()
        {
            return TestData.Localitys;
        }

        public LocalityModel GetOne(int id) => TestData.Localitys.First(l => l.Id == id);

        public LocalityModel GetOne(string name) => TestData.Localitys.Find(l => l.Name == name);
    }
}
