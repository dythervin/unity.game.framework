using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public interface IModelComponentOwner : IModel
    {
        new IModelComponentOwnerROData Data { get; }

        IReadOnlyObjectListOut<IModelComponent> Components { get; }
    }

    public interface IModelComponentOwnerInitializer : IModelComponentOwner
    {
        IComponentListInitializer ComponentListInitializer { get; }

        void ComponentsConstructed();
    }
}