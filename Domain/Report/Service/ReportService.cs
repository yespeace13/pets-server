using AutoMapper;
using ModelLibrary.Model.Report;
using ModelLibrary.View;
using OfficeOpenXml;
using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Report.Model;
using PetsServer.Domain.Report.Repository;
using PetsServer.Infrastructure.Services;

namespace PetsServer.Domain.Report.Service
{
    public class ReportService
    {
        private readonly ReportRepository _repository;
        public ReportService()
        {
            _repository = new ReportRepository();
        }

        public void Create(DateOnly from, DateOnly to)
        {
            var model = new ReportModel();
            model.DateStart = from;
            model.DateEnd = to;
            // на время
            model.Number = new Random().Next(1000);
            model.DateStatus = DateTime.Now;
            model.StatusId = 1;

            var acts = _repository.GetActs()
                .Where(a => a.DateOfCapture > from && a.DateOfCapture < to);
            var localitys = new Dictionary<int, ReportContentModel>();
            foreach (var act in acts)
            {
                if (localitys.ContainsKey(act.LocalityId))
                {
                    var content = localitys[act.LocalityId];
                    content.NumberOfAnimals += act.Animal.Count;
                    content.TotalCost += act.Animal.Count * act.Contract.ContractContent.First(cc => cc.LocalityId == act.LocalityId).Price;
                }
                else
                {
                    var content = new ReportContentModel();
                    content.LocalityId = act.LocalityId;
                    content.NumberOfAnimals = act.Animal.Count;
                    content.TotalCost = act.Animal.Count * act.Contract.ContractContent.First(cc => cc.LocalityId == act.LocalityId).Price;
                    localitys.Add(act.LocalityId, content);
                }
            }
            model.ReportContent = localitys.Values.ToList();
            _repository.Create(model);
        }

        public void Delete(int id)
        {
            var model = GetOne(id);
            _repository.Delete(model);
        }

        public ReportModel? GetOne(int id) => _repository.Get(id);

        public byte[] GenerateExcel(int id)
        {
            var model = Get(id);

            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Отчет");

            sheet.Cells[1, 1].Value = "Номер отчета:";
            sheet.Cells[1, 2].Value = model.Number;

            sheet.Cells[2, 1].Value = "Начало периода:";
            sheet.Cells[2, 2].Value = model.DateStart;
            sheet.Cells[2, 3].Value = "Конец периода:";
            sheet.Cells[2, 4].Value = model.DateEnd;


            sheet.Cells[3, 1].Value = "Идентификатор";
            sheet.Cells[3, 2].Value = "Населенный пункт";
            sheet.Cells[3, 3].Value = "Количество животных";
            sheet.Cells[3, 4].Value = "Общая стоимость руб.";

            var content = model.ReportContent.ToList();
            for (int i = 0; i < content.Count; i++)
            {
                sheet.Cells[i + 4, 1].Value = i + 1;
                sheet.Cells[i + 4, 2].Value = content[i].Locality.Name;
                sheet.Cells[i + 4, 3].Value = content[i].NumberOfAnimals;
                sheet.Cells[i + 4, 4].Value = content[i].TotalCost;
            }
            sheet.Cells[content.Count + 4, 2].Value = "Итого:";
            sheet.Cells[content.Count + 4, 3].Value = content.Sum(c => c.NumberOfAnimals);
            sheet.Cells[content.Count + 4, 4].Value = content.Sum(c => c.TotalCost);

            sheet.Cells[1, 1, content.Count + 4, 4].AutoFitColumns();

            return package.GetAsByteArray();
        }

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
    }
}
