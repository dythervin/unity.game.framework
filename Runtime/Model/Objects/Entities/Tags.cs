using Dythervin.Common;
using UnityEngine;

namespace Dythervin.Game.Framework
{
    public struct Tags
    {
        public static readonly Tag None = Tag.None;
        public static readonly Tag Player = Tag.Register(1, nameof(Player));
        public static readonly Tag Character = Tag.Register(2, nameof(Character));
        public static readonly Tag Vehicle = Tag.Register(3, nameof(Vehicle));
        public static readonly Tag Target = Tag.Register(4, nameof(Target));
        public static readonly Tag Empty = Tag.Register(5, nameof(Empty));
        public static readonly Tag Empty1 = Tag.Register(6, nameof(Empty1));

        [RuntimeInitializeOnLoadMethod]
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        private static void InitTags()
        {
        }
    }
}