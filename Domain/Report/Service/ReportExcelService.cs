using OfficeOpenXml;

namespace PetsServer.Domain.Report.Service;

public class ReportExcelService
{
    public byte[] GenerateExcel(int id)
    {
        var model = new ReportService().Get(id);

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
}
