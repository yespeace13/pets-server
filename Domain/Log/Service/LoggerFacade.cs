using PetsServer.Auth.Authorization.Model;

namespace PetsServer.Domain.Log.Service;

public class LoggerFacade(DbLogger dbLogger, TxtLogger txtLogger)
{
    private DbLogger _dbLogger = dbLogger;
    private TxtLogger _txtLogger = txtLogger;

    public void Log(UserModel user, Entities entity, Possibilities action, Type model, int? idEntity = null, int? idFile = null)
    {
        _dbLogger.Log(user, entity, action, model, idEntity, idFile);
        _txtLogger.Log(user, entity, action, model, idEntity, idFile);
    }
}
