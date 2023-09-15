namespace Dythervin.Game.Framework
{
    public interface IObjectListOut<out TObject> : IReadOnlyObjectListOut<TObject>, IObjectList
        where TObject : IObject
    {
    }
}