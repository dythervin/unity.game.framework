using Dythervin.Common;

namespace Dythervin.Game.Framework
{
    public static class LateInitializableExtensions
    {
        public static bool TryStart(this ILateInitializable awakable)
        {
            if (awakable.LateInitState != InitState.None)
                return false;

            awakable.LateInit();
            return true;
        }
    }
}