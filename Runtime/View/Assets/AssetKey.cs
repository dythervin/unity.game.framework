namespace Dythervin.Game.Framework.View
{
    public readonly struct AssetKey
    {
        public readonly string key;
        private readonly int hash;

        public AssetKey(string key, int hash)
        {
            this.key = key;
            this.hash = hash;
        }

        public override int GetHashCode()
        {
            return hash;
        }
    }
}