using IS_5;
using PetsServer.Authorization.Model;

namespace PetsServer.Organization.Service;

public class AuthorizationService
{
    
    public UserModel? GetUser(string login)
    {
        return TestData.Users.FirstOrDefault(u => u.Login == login);
    }

    
}

