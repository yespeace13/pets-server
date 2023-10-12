namespace PetsServer.Interfaces
{
    public interface IMapper<TModel, TView>
    {
        TView FromModelToView(TModel model);

        TModel FromViewToModel(TView view);
    }
}
