using Microsoft.EntityFrameworkCore;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Auth.Authorization.Service;

public class AuthorizationService
{
    private PetsContext _context = new();

    public static bool IsPossible(Possibilities possibility, Entities entity, UserModel user)
    {
        if (user != null)
        {
            var possibilities = user.Role.Possibilities.Where(r => r.Entity == entity);
            return possibilities.Any(p => p.Possibility == possibility);
        }
        return false;
    }

    public void AddUserRole(int userId, int roleId)
    {
        var user = _context.Users.Find(userId);
        if (user != null)
        {
            user.RoleId = roleId;
            _context.SaveChanges();
        }
        // Ошибки надо
    }

    public void DeleteUserRole(int userId)
    {
        var user = _context.Users.Find(userId);
        if (user != null)
        {
            user.RoleId = null;
            _context.SaveChanges();
        }
        // Ошибки надо
    }

    // TODO роли пользователя
    public List<EntityPossibilities>? GetUserPriviliges(int userId)
    {
        var userRole = _context.Users.Include(u => u.Role)
            .ThenInclude(r => r.Possibilities)
            .FirstOrDefault(id => id.Id == userId)?.Role;
        var userPossibilities = userRole?.Possibilities.ToList();
        return userPossibilities;
    }
}

