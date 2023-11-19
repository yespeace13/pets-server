using ModelLibrary.Model.Contract;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Repository;

namespace PetsServer.Domain.Contract.Service
{
    internal class ContractContentService
    {
        private ContractContentRepository _repository = new ContractContentRepository();
        internal void Delete(int id)
        {
            var entity = GetOne(id);
            _repository.Delete(entity);
        }

        internal IEnumerable<ContractContentModel> GetAll(int contractId)
        {
            return _repository.GetAll(contractId);
        }

        internal ContractContentModel GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        internal void Update(ContractContentModel entity)
        {
            var oldEntity = GetOne(entity.Id);
            oldEntity.Price = entity.Price;
            oldEntity.LocalityId = entity.LocalityId;
            _repository.Update(oldEntity);
        }

        internal void Create(ContractContentModel entity)
        {
            _repository.Create(entity);
        }
    }
}