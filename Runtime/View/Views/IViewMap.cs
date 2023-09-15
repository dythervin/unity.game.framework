using Dythervin.Collections;

namespace Dythervin.Game.Framework.View
{
    public interface IViewMap : IIndexerGetter<IObject, IModelView>
    {
        TView Get<TView>(IObject value)
            where TView : class;
        
        bool TryGet<TView>(IObject value, out TView view)
            where TView : class;

        //Dictionary<IView<IObject>, IObject>.Enumerator GetEnumerator();
        void Add(IObject value, IModelView view);
        bool Remove(IObject value);
    }
}