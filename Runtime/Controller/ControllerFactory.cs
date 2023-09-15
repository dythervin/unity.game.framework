using System;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework.View;
using Dythervin.ObjectPool;

namespace Dythervin.Game.Framework.Controller
{
    public class ControllerFactory<TModel, TController> : IControllerFactory<TController>
        where TController : class, IController
        where TModel : class, IModel
    {
        public Type ModelType => typeof(TModel);

        public event Action<IModel, IController> OnConstruct;

        private readonly bool _isViewDependant = typeof(TController).Implements(typeof(IControllerViewDependant));

        private readonly byte _constructorParamCount;

        public ControllerFactory()
        {
            _constructorParamCount = (byte)(_isViewDependant ? 2 : 1);
            if (!typeof(TController).IsInstantiatable())
                throw new Exception($"{typeof(TController)} must be non abstract");
        }

        public TController Construct(IModel model, IViewInputProvider inputProvider = null)
        {
            if (_isViewDependant && inputProvider == null)
                throw new NullReferenceException(nameof(inputProvider) + $" is null, {typeof(TController)} depends on it");

            using (ArrayPools.Get(_constructorParamCount, out object[] parameters))
            {
                parameters[0] = model;
                if (_constructorParamCount > 1)
                    parameters[1] = inputProvider;

                TController controller = (TController)Activator.CreateInstance(typeof(TController), parameters);
                OnConstruct?.Invoke(model, controller);
                return controller;
            }
        }

        IController IControllerFactory.Construct(IModel model, IViewInputProvider inputProvider)
        {
            return Construct(model, inputProvider);
        }
    }
}