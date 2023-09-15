using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IModelFactory 
    {
        // event Action<IModel> OnConstruct;
        //
        // event Action<IModel> OnInitialize;

        // void Init(IAnyFactoryControl anyFactory);

        IModel Construct(IModelData data);
    }


    public interface IModelFactory<out TModel> : IModelFactory
        where TModel : class, IModel
    {
        new TModel Construct(IModelData data);
    }


    public interface IModelFactory<out TModel, in TModelData> : IModelFactory<TModel>
        where TModel : class, IModel
        where TModelData : IModelData
    {
        TModel Construct(TModelData data);
    }
}