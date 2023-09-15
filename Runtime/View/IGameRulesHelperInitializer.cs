namespace Dythervin.Game.Framework.View
{
    public interface IGameRulesHelperInitializer : IGameRulesHelper
    {
        void Init(IRuleRepository ruleFactory, IModelContext context);
    }
}