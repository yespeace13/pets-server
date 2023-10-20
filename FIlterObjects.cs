using ModelLibrary.View;
using PetsServer.Contract.Model;
using PetsServer.Organization.Model;
using System.Reflection;

namespace PetsServer
{
    public class FilterObjects<T>
    {
        public IEnumerable<T> Filter(IEnumerable<T> models, string filtersQuery)
        {
            var filters = new FilterSetting(typeof(T));
            if (!String.IsNullOrEmpty(filtersQuery))
            {
                var filtersKeyValue = filtersQuery.Split(";");
                foreach (string filter in filtersKeyValue)
                {
                    var ketValue = filter.Split(":");
                    filters[ketValue[0]] = Uri.UnescapeDataString(ketValue[1]);
                }
            }
            if (filters.CountEmptyFileds == 0)
                return models;

            foreach (var column in filters.Columns)
            {
                var value = filters[column];
                if (value == null || value == "") continue;

                models = models.Where(m => m.GetType().GetProperty(column).GetValue(m) != null
                ? m.GetType().GetProperty(column).GetValue(m).ToString().Contains(value) : "".Contains(value));
            }
            return models;
        }
    }
}
