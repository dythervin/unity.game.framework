namespace Dythervin.Game.Framework
{
    public interface IEntityComponent : IModelComponent
    {
        new IEntity Owner { get; }

        void SetOwner(IEntity owner);
    }
}