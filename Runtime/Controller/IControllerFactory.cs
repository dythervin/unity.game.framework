using System;
using Dythervin.Game.Framework.View;
using JetBrains.Annotations;

namespace Dythervin.Game.Framework.Controller
{
    public interface IControllerFactory<out TController> : IControllerFactory
        where TController : class, IController
    {
        new TController Construct(IModel model, [CanBeNull] IViewInputProvider inputProvider = null);
    }

    public interface IControllerFactory
    {
        Type ModelType { get; }

        event Action<IModel, IController> OnConstruct;

        IController Construct(IModel model, [CanBeNull] IViewInputProvider inputProvider = null);
    }
}