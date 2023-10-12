
namespace IS_5
{
    public static class ExportDataToExcel
    {
        public static void Export<T>(string[] columns, string name, List<T> objects)
        {
            //Excel.Application app = new Excel.Application
            //{
            //    Visible = true,
            //    SheetsInNewWorkbook = 1
            //};
            //Workbook workBook = app.Workbooks.Add(Type.Missing);
            //app.DisplayAlerts = false;
            //Worksheet sheet = (Worksheet)app.Worksheets.get_Item(1);
            //for (int i = 0; i < columns.Length; i++)
            //    sheet.Cells[1, i + 1] = columns[i];
            //List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            //for (int i = 0; i < objects.Count; i++)
            //    for (int j = 0; j < properties.Count; j++)
            //        sheet.Cells[i + 2, j + 1] = objects[i].GetType().GetProperty(properties[j].Name).GetValue(objects[i]);
            //var cols = sheet.UsedRange.Columns;
            //cols.Columns.AutoFit();
            //app.Application.ActiveWorkbook.SaveAs(name);
            //sheet = null;
            //workBook.Close();
            //workBook = null;
            //app.Quit();
            //app = null;
        }
    }
}
