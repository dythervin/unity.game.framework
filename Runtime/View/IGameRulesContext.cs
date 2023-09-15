namespace Dythervin.Game.Framework.View
{
    public interface IGameRulesContext
    {
        IRuleRepository RuleRepository { get; }

        IModelContext Context { get; }

        IDataAssetRepository AssetRepository { get; }

        ViewFactory<TObserver, TView> DefaultViewFactory<TObserver, TView>()
            where TObserver : class, IModel
            where TView : class, IModelView;
    }
}