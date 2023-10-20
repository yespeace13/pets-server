using Microsoft.EntityFrameworkCore;
using PetsServer.Animal.Model;
using PetsServer.Infrastructure.Context;
using PetsServer.Organization.Model;

namespace PetsServer.Animal.Repository
{
    /**
     * Пока выглядит немного убого, скорее всего буду переделывать
    */
    public class AnimalRepository
    {

        public AnimalModel GetOne(int id)
        {
            using var context = new PetsContext();
            return context.Animals.Where(o => o.Id == id)
                .First();
        }

        public List<AnimalModel> GetAll()
        {
            using var context = new PetsContext();
            return context.Animals.ToList();
        }

        public void Create(AnimalModel animal)
        {
            using var context = new PetsContext();
            context.Add(animal);
            context.SaveChanges();
        }

        public void Update(AnimalModel animal)
        {
            using var context = new PetsContext();
            var oldAnim = context.Animals.Where(o => o.Id == animal.Id)
                .First();
            oldAnim.Category = animal.Category;
            oldAnim.Sex = animal.Sex;
            oldAnim.Breed = animal.Breed;
            oldAnim.Size = animal.Size;
            oldAnim.Wool = animal.Wool;
            oldAnim.Color = animal.Color;
            oldAnim.Ears = animal.Ears;
            oldAnim.Tail = animal.Tail;
            oldAnim.SpecialSigns = animal.SpecialSigns;
            oldAnim.IdentificationLabel = animal.IdentificationLabel;
            oldAnim.ChipNumber = animal.ChipNumber;
            context.Animals.Update(oldAnim);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            using var context = new PetsContext();
            var animal = context.Animals.Where(o => o.Id == id)
                .First();
            context.Animals.Remove(animal);
            context.SaveChanges();
        }
    }
}
