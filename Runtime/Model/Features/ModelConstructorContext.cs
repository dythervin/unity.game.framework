namespace Dythervin.Game.Framework
{
    public class ModelConstructorContext : IModelConstructorContext
    {
        public IFeature Feature { get; }

        public IModelInitIListener InitListener { get; }

        public ModelConstructorContext(IFeature feature, IModelInitIListener initListener)
        {
            Feature = feature;
            InitListener = initListener;
        }
    }
}