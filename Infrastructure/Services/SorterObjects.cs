using ModelLibrary.View;

namespace PetsServer.Infrastructure.Services
{
    public class SorterObjects<T>
    {
        public IEnumerable<T> SortField(IEnumerable<T> models, string? sortField, int? sortType)
        {
            var sort = new SortSettings(sortField, 0);

            if (sortType.HasValue) sort.Direction = sortType.Value;
            if (string.IsNullOrEmpty(sortField)) sortField = "Id";

            IEnumerable<T> result = sort.Direction == 0 ?
                    models.OrderBy(m => m.GetType()?.GetProperty(sortField)?.GetValue(m))
                    : models.OrderByDescending(m => m?.GetType()?.GetProperty(sortField)?.GetValue(m));

            return result;
        }
    }
}
