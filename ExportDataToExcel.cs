using OfficeOpenXml;
using System.Reflection;

namespace PetsServer
{
    public static class ExportDataToExcel
    {
        public static byte[] Export<T>(string name, List<T> objects)
        {
            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add(name);


            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            for (int i = 0; i < properties.Count; i++)
                sheet.Cells[1, i + 1].Value = properties[i].CustomAttributes.First().ConstructorArguments.First().Value;

            for (int i = 0; i < objects.Count; i++)
                for (int j = 0; j < properties.Count; j++)
                    sheet.Cells[i + 2, j + 1].Value = objects[i].GetType().GetProperty(properties[j].Name).GetValue(objects[i]);

            sheet.Cells[1, 1, objects.Count + 1, properties.Count + 2].AutoFitColumns();

            return package.GetAsByteArray();


        }
    }
}
