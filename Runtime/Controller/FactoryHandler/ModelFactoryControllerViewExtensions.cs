using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Game.Framework.View;

namespace Dythervin.Game.Framework.Controller
{
    public static class ModelFactoryControllerViewExtensions
    {
        public static IReadOnlyList<IFeatureFactoryView> SetViewFactory(
            this IReadOnlyList<IFeatureFactoryView> list, IViewFactory viewFactory)
        {
            foreach (IFeatureFactoryView observerFactoryControllerView in list.ToEnumerable())
            {
                observerFactoryControllerView.SetViewFactory(viewFactory);
            }

            return list;
        }
    }
}