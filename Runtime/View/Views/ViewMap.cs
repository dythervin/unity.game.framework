using System.Collections.Generic;

namespace Dythervin.Game.Framework.View
{
    public class ViewMap : Dictionary<IObject, IModelView>, IViewMap
    {
        public TView Get<TView>(IObject value)
            where TView : class
        {
            return (TView)this[value];
        }

        public bool TryGet<TView>(IObject value, out TView view)
            where TView : class
        {
            if (TryGetValue(value, out IModelView viewBase))
            {
                view = (TView)viewBase;
                return true;
            }

            view = null;
            return false;
        }
    }
}