using AutoMapper;
using ModelLibrary.Model.Papka;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Log.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Log.Service;

public class LogService
{
    private Type? _entity;
    public LogService(Type entity) => _entity = entity;
    public LogService() { }

    private LogRepository _repository = new LogRepository();

    public void Delete(int id)
    {
        var model = GetOne(id);
        if (model == null) return;
        _repository.Delete(model);
    }

    public LogModel? GetOne(int id) => _repository.Get(id);

    public PageSettings<LogViewList> GetPage(int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<LogViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        // берем модели по этим правилам
        var models = _repository.Get();

        //// Фильтрация
        //organizationsView = new FilterObjects<OrganizationViewList>().Filter(organizationsView, filter);

        var views = mapper.Map<IEnumerable<LogViewList>>(models);

        // Сортировка
        views = new SorterObjects<LogViewList>().SortField(views, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)views.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = views
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    //public byte[] ExportToExcel(string filters, IMapper mapper)
    //{
    //    IEnumerable<LogModel> models = mapper.Map<List<OrganizationViewList>>(_repository.Get());
    //    models = new FilterObjects<OrganizationViewList>().Filter(models, filters);
    //    return ExportDataToExcel.Export(
    //        "Организации", models.ToList());
    //}

    /// <summary>
    /// Метод для логирования записи, которую изменили
    /// Обязательно заполнить один из иденитификатор, иначе ошибка
    /// </summary>
    /// <param name="user">Пользователь изменивший запись</param>
    /// <param name="idEntity">Идентификатор записи(не обязательно)</param>
    /// <param name="idFile">Идентификатор файла(не обязательно)</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void LogData(UserModel user, int? idEntity = null, int? idFile = null)
    {
        if (_entity == null) throw new ArgumentNullException("Не для того");
        if(idEntity == null && idFile == null) throw new ArgumentNullException("Не ввели идентификатор");
        var properties = _entity.GetProperties().Select(p => p.Name);
        var sepProp = String.Join(';', properties);

        var log = new LogModel
        {
            ActionDate = DateTime.Now,
            UserId = user.Id,
            ObjectId = idEntity,
            Entity = sepProp,
            FileId = idFile
        };
        _repository.Create(log);
    }
}

