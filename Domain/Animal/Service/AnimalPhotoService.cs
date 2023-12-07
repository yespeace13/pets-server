﻿using ModelLibrary.Model.Etc;
using PetsServer.Domain.Animal.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Animal.Service;

public class AnimalPhotoService
{
    private PetsContext _context = new PetsContext();

    public void AddPhoto(int animalId, IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var path = Path.Combine("Files", fileName);
        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
        _context.Add(new AnimalPhoto
        {
            ParentId = animalId,
            Path = path
        });
        _context.SaveChanges();
    }

    public void DeletePhoto(int id)
    {
        var photo = _context.AnimalPhotos.FirstOrDefault(p => p.Id == id);
        if (photo == null) return;
        File.Delete(photo.Path);

        _context.AnimalPhotos.Remove(photo);
        _context.SaveChanges();
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

