using ModelLibrary.Model.Contract;
using PetsServer.Domain.Plan.Model;
using PetsServer.Domain.Plan.Repository;

namespace PetsServer.Domain.Plan.Service
{
    internal class PlanContentService
    {
        private PlanContentRepository _repository = new PlanContentRepository();
        internal void Delete(int id)
        {
            var entity = GetOne(id);
            _repository.Delete(entity);
        }

        internal IEnumerable<PlanContentModel> GetAll(int planId)
        {
            return _repository.GetAll(planId);
        }

        internal PlanContentModel GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        internal void Update(PlanContentModel entity)
        {
            var oldEntity = GetOne(entity.Id);
            oldEntity.Day = entity.Day;
            oldEntity.LocalityId = entity.LocalityId;
            oldEntity.ActId = entity.ActId;
            oldEntity.Adress = entity.Adress;
            oldEntity.Check = entity.Check;
            _repository.Update(oldEntity);
        }

        internal void Create(PlanContentModel entity)
        {
            _repository.Create(entity);
        }
    }
}