using System.Collections.Generic;
using Dythervin.Collections;

namespace Dythervin.Game.Framework.Controller
{
    public interface IControllerMap
    {
        bool TryGet(IModel model, out IReadOnlyList<IController> controllers);
    }

    public class ControllerMap : MultiDictionary<IModel, IController, HashList<IController>>, IControllerMap
    {
        public bool TryGet(IModel model, out IReadOnlyList<IController> controllers)
        {
            if (TryGetValue(model, out var value))
            {
                controllers = value;
                return true;
            }

            controllers = null;
            return false;
        }
    }
}