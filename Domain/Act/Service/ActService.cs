using PetsServer.Contract.Model;
using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Act.Repository;

namespace PetsServer.Domain.Act.Service;

public class ActService
{
    private ActRepository _repository = new ActRepository();

    public void Create(ActModel model)
    {
        _repository.Create(model);
    }

    public void Update(ActModel model)
    {
        var oldModel = GetOne(model.Id);
        oldModel.AnimalId = model.AnimalId;
        oldModel.OrganizationId = model.OrganizationId;
        oldModel.DateOfCapture = model.DateOfCapture;
        _repository.Update(oldModel);
    }

    public void Delete(int id)
    {
        var organization = GetOne(id);
        _repository.Delete(organization);
    }

    public ActModel GetOne(int id)
    {
        throw new NotImplementedException();
    }

    //public ActModel? GetOne(int id) => _repository.GetOne(id);
}
