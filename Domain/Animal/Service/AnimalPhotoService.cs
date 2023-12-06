using PetsServer.BaseFile.Service;
using PetsServer.Domain.Animal.Model;
using PetsServer.Infrastructure.Context;
using System.Drawing;

namespace PetsServer.Domain.Animal.Service;

public class AnimalPhotoService
{
    private PetsContext _context = new PetsContext();
    
    public void AddPhoto(int animalId, byte[] file)
    {
        var path = "Files\\" + DateTime.Now.ToString();
        File.WriteAllBytes(path, file);

        _context.AnimalPhotos.Add(new AnimalPhoto
        {
            ParentId = animalId,
            Path = path
        });
    }

    //public void Delete(int id)
    //{
    //    var organization = GetOne(id);
    //    _repository.Delete(organization);
    //}

    //public AnimalModel? GetOne(int id) => _repository.GetOne(id);

}

