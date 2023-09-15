using Zenject;

namespace Dythervin.Game.Framework.View
{
    public abstract class GameRulesHelper : IGameRulesHelperInitializer
    {
        public IRuleRepository RuleFactory { get; private set; }

        public IModelContext Context { get; private set; }

        [Inject] public IGameObjectFactory GameObjectFactory { get; private set; }

        [Inject] public IViewMap ViewMap { get; private set; }

        [Inject] public IViewContext ViewContext { get; private set; }

        void IGameRulesHelperInitializer.Init(IRuleRepository ruleFactory, IModelContext context)
        {
            RuleFactory = ruleFactory;
            Context = context;
        }

        [Inject]
        protected virtual void Init(DiContainer container)
        {
        }
    }
}