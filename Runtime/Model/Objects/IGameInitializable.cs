using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public interface IGameInitializable : IInitializable
    {
        protected internal void InitInternal();
    }
}