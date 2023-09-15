using System.Collections.Generic;
using Dythervin.Collections;

namespace Dythervin.Game.Framework.Controller
{
    public static class ModelFactoryHandlerControllerExtensions
    {
        public static IReadOnlyList<IFeatureFactoryController> SetViewFactory(
            this IReadOnlyList<IFeatureFactoryController> list, IControllerFactory controllerFactory)
        {
            foreach (IFeatureFactoryController observerFactoryControllerView in list.ToEnumerable())
            {
                observerFactoryControllerView.SetController(controllerFactory);
            }

            return list;
        }
    }
}