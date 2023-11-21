using IS_5;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Infrastructure.Context;
using System.Data;

namespace PetsServer.Auth.Authorization.Service;

public class AuthorizationUserService
{
    private PetsContext _context;

    public AuthorizationUserService() => _context = new PetsContext();

    public static bool IsPossible(Possibilities possibility, Entities entity, UserModel user)
    {
        if (user != null && user.Role.Possibilities.FirstOrDefault(r => r.Entity == entity)?.Possibility == possibility)
            return true;
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
    public Dictionary<string, Possibilities> GetUserPossibilities(int userId)
    {
        //var userRole = _context.Users.FirstOrDefault(id => id.Id == userId)?.RoleId;
        //var possipilities = _context.EntityRestrictions.Where(r => r.RoleId == userRole)
        //    .Select(r => r.Possibility)
        //    .ToList();
        return null;
    }
}

