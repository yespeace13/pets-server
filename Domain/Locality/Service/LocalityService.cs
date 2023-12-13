using PetsServer.Domain.Locality.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Domain.Locality.Service;

public class LocalityService
{
    private readonly PetsContext _context = new();

    public List<LocalityModel> GetLocalitys() => _context.Localities.ToList();

    public LocalityModel? GetOne(int id) => _context.Localities.Find(id);

}