using PetsServer.Domain.Contract.Model;
using PetsServer.Domain.Contract.Repository;

namespace PetsServer.Domain.Contract.Service;

internal class ContractContentService
{
    private readonly ContractContentRepository _repository = new();
    
    internal IQueryable<ContractContentModel> GetAllContentByContractId(int contractId)
    {
        return _repository.GetContractAll(contractId);
    }
}