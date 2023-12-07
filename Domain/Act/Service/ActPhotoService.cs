using ModelLibrary.Model.Etc;
using PetsServer.Domain.Act.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Act.Service;

public class ActPhotoService
{
    private PetsContext _context = new PetsContext();

    public void AddPhoto(int animalId, IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine("Files", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        _context.ActPhotos.Add(new ActPhoto
        {
            ParentId = animalId,
            Path = path
        });
        _context.SaveChanges();
    }

    public void DeletePhoto(int id)
    {
        var photo = _context.ActPhotos.FirstOrDefault(p => p.Id == id);
        if (photo == null) return;
        File.Delete(photo.Path);

        _context.ActPhotos.Remove(photo);
        _context.SaveChanges();
    }

    public List<FileBaseView> Get(int animalId)
    {
        var photos = _context.ActPhotos.Where(p => p.ParentId == animalId);
        return photos.Select(p => new FileBaseView
        {
            Id = p.Id,
            File = File.ReadAllBytes(p.Path)
        }).ToList();
    }

}

