using Dythervin.Core;

namespace Dythervin.Game.Framework
{
    public interface IModelInitIListener : IListener<IModel>
    {
    }

    public interface IModelConstructorContext
    {
        IFeature Feature { get; }

        IModelInitIListener InitListener { get; }
    }
}