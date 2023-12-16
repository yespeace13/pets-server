using PetsServer.Auth.Authorization.Model;

namespace PetsServer.Domain.Log.Service;

public class TxtLogger
{
    public void Log(UserModel user, Entities entity, Possibilities action, Type model, int? idEntity = null, int? idFile = null)
    {

    }
}
