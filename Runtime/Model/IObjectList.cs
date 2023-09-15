namespace Dythervin.Game.Framework
{
    public interface IObjectList : IReadOnlyObjectContainer
    {
        bool Add(IObject obj);

        bool Remove(IObject obj);
    }

    public interface IObjectList<TObject> : IObjectListOut<TObject>, IReadOnlyObjectList<TObject>, IObjectList
        where TObject : IObject
    {
        bool Add(TObject obj);

        bool Remove(TObject obj);
    }
}