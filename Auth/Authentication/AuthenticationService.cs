using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Auth.Authentication
{
    public class AuthenticationService
    {
        private readonly PetsContext _context = new();
        private readonly PasswordHasher<UserModel> _passwordHasher = new();

        public UserModel? GetUser(string login)
        {
            return _context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.Possibilities)
                .Include(u => u.Locality)
                .Include(u => u.Organization)
                .FirstOrDefault(u => u.Login == login);
        }

        public List<UserModel> GetUsers()
        {
            return [.. _context.Users];
        }

        public void CreateUser(UserModel user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

        }
    }
}
