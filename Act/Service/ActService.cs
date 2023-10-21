
using PetsServer.Act.Model;
using PetsServer.Act.Repository;
using PetsServer.Contract.Model;

namespace PetsServer.Act.Service;

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

    public ActModel? GetOne(int id) => _repository.GetOne(id);
}
