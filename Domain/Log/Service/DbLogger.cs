using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Log.Repository;

namespace PetsServer.Domain.Log.Service;

public class DbLogger
{
    private readonly LogRepository _repository = new();
    public void Log(UserModel user, Entities entity, Possibilities action, Type model, int? idEntity = null, int? idFile = null)
    {
        if (idEntity == null && idFile == null) throw new ArgumentNullException("Не ввели идентификатор");
        var properties = model.GetProperties().Select(p => p.Name);
        var sepProp = String.Join(';', properties);

        var log = new LogModel
        {
            ActionDate = DateTime.Now,
            UserId = user.Id,
            ObjectId = idEntity,
            EntityDescription = sepProp,
            FileId = idFile,
            Action = action,
            Entity = entity,
        };
        _repository.Create(log);
    }
}
