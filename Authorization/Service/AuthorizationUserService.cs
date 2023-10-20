using IS_5;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsServer.Authorization.Model;
using PetsServer.Infrastructure.Context;
using System.Data;

namespace PetsServer.Organization.Service;

public class AuthorizationUserService
{
    private PetsContext _context;

    public AuthorizationUserService() => _context = new PetsContext();
    
    public static bool IsPossible(Possibilities possibility, Entities entity, UserModel user)
    {
        if (user != null && user.Role.Possibilities.FirstOrDefault(r => r.Entity == entity).Possibility == possibility)
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
}

