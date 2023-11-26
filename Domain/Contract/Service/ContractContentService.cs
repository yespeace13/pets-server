using ModelLibrary.Model.Contract;
using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Repository;

namespace PetsServer.Domain.Contract.Service
{
    internal class ContractContentService
    {
        private ContractContentRepository _repository = new ContractContentRepository();
        
        internal IQueryable<ContractContentModel> GetContractAll(int contractId)
        {
            return _repository.GetContractAll(contractId);
        }
    }
}