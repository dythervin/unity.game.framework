using System;

namespace Dythervin.Game.Framework
{
    public interface IRuleFactoryMap : IFeatureFactoryMap<IFeatureFactory>
    {
        event Action<IFeatureFactory> OnAdded;
    }

    public interface IRuleRepository : IRuleFactoryMap
    {
        void Register(IFeatureFactory featureFactory, bool @override = false);

        TObserverFactoryController Register<TObserverFactoryController>(TObserverFactoryController featureFactory, bool @override = false)
            where TObserverFactoryController : class, IFeatureFactory;

        void Clear();
    }
}