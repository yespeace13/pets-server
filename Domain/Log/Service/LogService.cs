using AutoMapper;
using ModelLibrary.Model.LogInformation;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Log.Model;
using PetsServer.Domain.Log.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Log.Service;

public class LogService : ILog
{
    private readonly Type? _entity;
    public LogService(Type entity) => _entity = entity;
    public LogService() { }

    private readonly LogRepository _repository = new();

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
        //models = Filter(models, filter);

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

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<LogViewList> views = mapper.Map<List<LogViewList>>(_repository.Get());
        views = new FilterObjects<LogViewList>().Filter(views, filters);
        return ExportDataToExcel.Export(
            "Журнал", views.ToList());
    }

    /// <summary>
    /// Метод для логирования записи, которую изменили
    /// Обязательно заполнить один из иденитификатор, иначе ошибка
    /// </summary>
    /// <param name="user">Пользователь изменивший запись</param>
    /// <param name="idEntity">Идентификатор записи(не обязательно)</param>
    /// <param name="idFile">Идентификатор файла(не обязательно)</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// проверка имени 2

    public void Log(UserModel user, int? idEntity = null, int? idFile = null)
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

    //private IQueryable<LogModel> Filter(IQueryable<LogModel> models, string filtersQuery)
    //{
    //    if (string.IsNullOrEmpty(filtersQuery)) return models;

    //    var filters = new FilterSetting(typeof(LogViewList));

    //    // TODO потом отдельна куда-нибудь вывести
    //    var filtersKeyValue = filtersQuery.Split(";");
    //    foreach (string filter in filtersKeyValue)
    //    {
    //        var ketValue = filter.Split(":");
    //        filters[ketValue[0]] = Uri.UnescapeDataString(ketValue[1]);
    //    }

    //    if (filters.CountEmptyFileds == 0)
    //        return models;

    //    foreach (var filter in filters.Columns)
    //    {
    //        switch (filter)
    //        {
    //            case "Id":
    //                models = models.Where(m => m.Id.ToString().Contains(filters[filter]));
    //                break;
    //            case "UserLastName":
    //                models = models.Where(m => m.User.LastName.Contains(filters[filter]));
    //                break;
    //            case "UserFirstName":
    //                models = models.Where(m => m.User.FirstName.Contains(filters[filter]));
    //                break;
    //            case "UserPatronymic":
    //                models = models.Where(m => m.User.Patronymic.Contains(filters[filter]));
    //                break;
    //            case "PhoneNumber":
    //                models = models.Where(m => m.User.Phone.Contains(filters[filter]));
    //                break;
    //            case "Email":
    //                models = models.Where(m => m.User.Email.Contains(filters[filter]));
    //                break;
    //            case "NameOrganization":
    //                models = models.Where(m => m.User.Organization.NameOrganization.Contains(filters[filter]));
    //                break;
    //            case "StructuralUnit":
    //                models = models.Where(m => m.User.Department.Contains(filters[filter]));
    //                break;
    //            case "Post":
    //                models = models.Where(m => m.User.Position.Contains(filters[filter]));
    //                break;
    //            case "WorkPhoneNumber":
    //                models = models.Where(m => m.User.Organization.Phone.Contains(filters[filter]));
    //                break;
    //            case "WorkEmail":
    //                models = models.Where(m => m.User.Organization.Email.Contains(filters[filter]));
    //                break;
    //            case "Login":
    //                models = models.Where(m => m.User.Login.Contains(filters[filter]));
    //                break;
    //            case "ActionDate":
    //                var periodDate = filters[filter].Split(' ');
    //                if (DateTime.TryParse(periodDate[0], out var startDate1) && DateTime.TryParse(periodDate[1], out var endDate1))
    //                    models = models.Where(m => m.ActionDate >= startDate1 && m.ActionDate <= endDate1);
    //                break;
                
    //            case "Entity":
    //                models = models.Where(m => m.Entity.Contains(filters[filter]));
    //                break;
    //            case "ObjectId":
    //                models = models.Where(m => m.ObjectId.ToString().Contains(filters[filter]));
    //                break;
    //            case "FileId":
    //                models = models.Where(m => m.FileId.ToString().Contains(filters[filter]));
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    return models;
    //}

    public void Log(UserModel user, Entities entity, Possibilities action, int? idEntity = null, int? idFile = null)
    {
        throw new NotImplementedException();
    }
}

