namespace Dythervin.Game.Framework.View
{
    public interface IGameRules<in T>
        where T : IGameRulesHelper
    {
        void Construct(IGameRulesContext context, T helper);
    }
}