using AutoMapper;
using ModelLibrary.Model.Report;
using ModelLibrary.View;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Report.Model;
using PetsServer.Domain.Report.Repository;
using PetsServer.Infrastructure.Context;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Report.Service;

public class ReportService
{
    private readonly ReportRepository _repository = new();

    public int Create(DateOnly from, DateOnly to)
    {
        var model = new ReportModel();
        model.DateStart = from;
        model.DateEnd = to;
        // на время
        model.Number = new Random().Next(10000);
        model.DateStatus = DateTime.Now;
        model.StatusId = 1;

        var acts = _repository.GetActs()
            .Where(a => a.DateOfCapture > from && a.DateOfCapture < to);
        var localitys = new Dictionary<int, ReportContentModel>();
        foreach (var act in acts)
        {
            if (localitys.TryGetValue(act.LocalityId, out ReportContentModel? value))
            {
                var content = value;
                content.NumberOfAnimals += act.Animal.Count;
                content.TotalCost += act.Animal.Count * act.Contract.ContractContent.First(cc => cc.LocalityId == act.LocalityId).Price;
            }
            else
            {
                var content = new ReportContentModel
                {
                    LocalityId = act.LocalityId,
                    NumberOfAnimals = act.Animal.Count,
                    TotalCost = act.Animal.Count * act.Contract.ContractContent.First(cc => cc.LocalityId == act.LocalityId).Price
                };
                localitys.Add(act.LocalityId, content);
            }
        }
        model.ReportContent = localitys.Values.ToList();
        _repository.Create(model);
        return model.Id;
    }

    public void Delete(int id)
    {
        var model = GetOne(id);
        _repository.Delete(model);
    }

    public ReportModel? GetOne(int id) => _repository.Get(id);

    public PageSettings<ReportViewList> Get(int? pageQuery, int? limitQuery, string? filter, string? sortField, int? sortType, UserModel user, IMapper mapper)
    {
        // создаем страницу с настройками
        var pageSettings = new PageSettings<ReportViewList>();

        // проверяем, что передано значение для номера страницы
        if (pageQuery.HasValue && pageQuery > 0)
            pageSettings.Page = pageQuery.Value;

        // проверяем, что есть значение для объема страницы
        if (limitQuery.HasValue && limitQuery.Value > 0)
            pageSettings.Limit = limitQuery.Value;

        var models = _repository.Get();

        //// Фильтрация
        //organizationsView = new FilterObjects<OrganizationViewList>().Filter(organizationsView, filter);

        var views = mapper.Map<IEnumerable<ReportViewList>>(models);

        // Сортировка
        views = new SorterObjects<ReportViewList>().SortField(views, sortField, sortType);

        // Количество страниц всего
        pageSettings.Pages = (int)Math.Ceiling((double)views.Count() / pageSettings.Limit);

        // Добавляем элементы в страницу с необходимым количеством
        pageSettings.Items = views
            .Skip(pageSettings.Limit * (pageSettings.Page - 1))
            .Take(pageSettings.Limit);

        return pageSettings;
    }

    public ReportModel? Get(int id)
    {
        return _repository.Get(id);
    }

    public byte[] ExportToExcel(string filters, IMapper mapper)
    {
        IEnumerable<ReportViewList> views = mapper.Map<List<ReportViewList>>(_repository.Get());
        views = new FilterObjects<ReportViewList>().Filter(views, filters);
        return ExportDataToExcel.Export(
            "Отчеты", views.ToList());
    }

    internal void SetReportStatus(int reportId, int statusId)
    {
        var report = _repository.Get(reportId);
        report.StatusId = statusId;
        _repository.Update(report);
    }

    internal List<ReportStatusModel>? GetStatuses(int reportId, UserModel user)
    {
        var statuses = new PetsContext().ReportsStatuses;
        var report = _repository.Get(reportId);
        var role = user.Role.Name;
        var reportStatus = report.Status.StatusName;

        if (role == "super-man") return [.. statuses];

        if (role == "Оператор ОМСУ")
        {
            if(reportStatus == "Черновик")
            {
                return [.. statuses.Where(s => s.StatusName == "Черновик"
                    || s.StatusName == "Согласование у исполнителя Муниципального Контракта"
                    || s.StatusName == "Доработка")];
            }
            if(reportStatus == "Доработка")
            {
                return [.. statuses.Where(s => s.StatusName == "Согласование у исполнителя Муниципального Контракта"
                    || s.StatusName == "Доработка")];
            }
        }

        if (role == "Куратор ОМСУ")
        {
            if (reportStatus == "Согласование у исполнителя Муниципального Контракта")
            {
                return [.. statuses.Where(s => s.StatusName == "Доработка"
                    || s.StatusName == "Согласован у исполнителя Муниципального Контракта")];
            }

            if (reportStatus == "Утвержден у исполнителя Муниципального Контракта")
            {
                return [.. statuses.Where(s => s.StatusName == "Доработка"
                    || s.StatusName == "Согласован в ОМСУ")];
            }
        }

        if (role == "Подписант ОМСУ")
        {
            if(reportStatus == "Согласован у исполнителя Муниципального Контракта")
            {
                return [.. statuses.Where(s => s.StatusName == "Доработка"
                    || s.StatusName == "Утвержден у исполнителя Муниципального Контракта")];
            }
        }
            
        return [report.Status];
    }

    internal ReportStatusModel? GetActualStatus(int reportId)
    {
        return Get(reportId)?.Status;
    }
}

/*
 * ('Черновик'),
('Доработка'),
('Согласование у исполнителя Муниципального Контракта'),
('Согласован у исполнителя Муниципального Контракта'),
('Утвержден у исполнителя Муниципального Контракта'),
('Согласован в ОМСУ');
 */
