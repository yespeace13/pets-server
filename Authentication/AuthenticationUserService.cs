using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetsServer.Authorization.Model;
using PetsServer.Infrastructure.Context;

namespace PetsServer.Authentication
{
    public class AuthenticationUserService
    {
        private PetsContext _context;
        private PasswordHasher<UserModel> _passwordHasher;

        public AuthenticationUserService()
        {
            _context = new PetsContext();


            _passwordHasher = new PasswordHasher<UserModel>();
        }

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
            return _context.Users.ToList();
        }

        public void CreateUser(UserModel user)
        {
            try
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch
            {
                 // ToDO нужно добавить возврат ошибок!!!
            }
        }

        public void UpdateUser(UserModel user)
        {
            //_context.Users.Add(user);
            //_context.SaveChanges();
        }

        private void DeleteUser(UserModel user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
