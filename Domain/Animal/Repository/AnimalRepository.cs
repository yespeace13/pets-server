using PetsServer.Domain.Animal.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Animal.Repository
{
    /**
     * Пока выглядит немного убого, скорее всего буду переделывать
    */
    public class AnimalRepository
    {
        private PetsContext _context = new PetsContext();
        public AnimalModel GetOne(int id)
        {
            return _context.Animals.Where(o => o.Id == id)
                .First();
        }

        public List<AnimalModel> GetAll()
        {
            return _context.Animals.ToList();
        }

        public void Create(AnimalModel animal)
        {
            _context.Add(animal);
            _context.SaveChanges();
        }

        public void Update(AnimalModel animal)
        {
            var oldAnim = GetOne(animal.Id);
            
            _context.Animals.Update(oldAnim);
            _context.SaveChanges();
        }

        public void Delete(AnimalModel animal)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }
    }
}
