namespace Dythervin.Game.Framework.Controller
{
    public interface IFeatureFactoryController : IFeatureFactory
    {
        IFeatureFactoryController SetController(IControllerFactory controllerFactory);
    }
}