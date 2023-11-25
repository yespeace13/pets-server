using PetsServer.Domain.Report.Model;
using PetsServer.Domain.Report.Repository;

namespace PetsServer.Domain.Report.Service
{
    public class ReportService
    {
        private ReportRepository _repository;
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

        public IEnumerable<ReportModel> Get()
        {
            return _repository.Get();
        }

        public ReportModel? Get(int id)
        {
            return _repository.Get(id);
        }
    }
}
