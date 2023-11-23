using AutoMapper;
using ModelLibrary.Model.Animal;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Animal.Model;
using PetsServer.Domain.Animal.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Animal.Service;

public class AnimalService
{
    private AnimalRepository _repository = new AnimalRepository();

    public void Create(AnimalModel model)
    {
        _repository.Create(model);
    }

    public void Update(AnimalModel model)
    {
        var oldModel = GetOne(model.Id);
        oldModel.Category = model.Category;
        oldModel.Sex = model.Sex;
        oldModel.Breed = model.Breed;
        oldModel.Size = model.Size;
        oldModel.Wool = model.Wool;
        oldModel.Color = model.Color;
        oldModel.Ears = model.Ears;
        oldModel.Tail = model.Tail;
        oldModel.SpecialSigns = model.SpecialSigns;
        oldModel.IdentificationLabel = model.IdentificationLabel;
        oldModel.ChipNumber = model.ChipNumber;
        _repository.Update(oldModel);
    }

    public void Delete(int id)
    {
        var organization = GetOne(id);
        _repository.Delete(organization);
    }

    public AnimalModel? GetOne(int id) => _repository.GetOne(id);

    public PageSettings<AnimalViewList> GetPage(
        int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<AnimalViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        // берем организации по этим правилам
        var contracts = _repository.GetAll();

        var userRestiction = user.Role.Possibilities.Where(p => p.Entity == Entities.Organization && p.Possibility == Possibilities.Read).First().Restriction;

        IEnumerable<AnimalViewList> contractsView = mapper.Map<List<AnimalViewList>>(contracts);
        // Фильтрация
        contractsView = new FilterObjects<AnimalViewList>().Filter(contractsView, filter);

        // Сортировка
        contractsView = new SorterObjects<AnimalViewList>().SortField(contractsView, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)contractsView.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = contractsView
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<AnimalViewList> organizations = mapper.Map<List<AnimalViewList>>(_repository.GetAll());
        organizations = new FilterObjects<AnimalViewList>().Filter(organizations, filters);
        return ExportDataToExcel.Export(
            "Организации", organizations.ToList());
    }
}

