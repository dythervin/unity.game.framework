using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public interface ILateInitializable
    {
        InitState LateInitState { get; }

        void LateInit();
    }
}