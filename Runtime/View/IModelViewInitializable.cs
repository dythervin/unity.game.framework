using Dythervin.Common;

namespace Dythervin.Game.Framework.View
{
    public interface IModelViewInitializable : IViewInitializable, IModelView
    {
        InitState InitState { get; }

        void Init(IModel objectExt, IViewContext viewContext);
    }
}