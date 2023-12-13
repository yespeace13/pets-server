using ModelLibrary.Model.Etc;
using PetsServer.Domain.Act.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Act.Service;

public class AnimalPhotoService
{
    private readonly PetsContext _context = new();

    public int AddPhoto(int animalId, IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine("Files", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        var animalPhoto = new AnimalPhoto
        {
            ParentId = animalId,
            Path = path
        };
        _context.Add(animalPhoto);
        _context.SaveChanges();
        return animalPhoto.Id;
    }

    public int DeletePhoto(int id)
    {
        var photo = _context.AnimalPhotos.FirstOrDefault(p => p.Id == id);
        if (photo == null) return -1;
        File.Delete(photo.Path);

        _context.AnimalPhotos.Remove(photo);
        _context.SaveChanges();
        return photo.ParentId;
    }

    public List<FileBaseView> Get(int animalId)
    {
        var photos = _context.AnimalPhotos.Where(p => p.ParentId == animalId);
        return photos.Select(p => new FileBaseView
        {
            Id = p.Id,
            File = File.ReadAllBytes(p.Path)
        }).ToList();
    }
}

