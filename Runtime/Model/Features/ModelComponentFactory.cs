using Dythervin.Game.Framework.Data;


namespace Dythervin.Game.Framework
{
    public abstract class ModelComponentFactory<TComponent, TData> : ModelFactory<TComponent, TData>
        where TComponent : class, IModelComponent, IModelInitializable
        where TData : class, IModelComponentData
    {
        protected ModelComponentFactory(IModelContext context) : base(context)
        {
        }
    }
}