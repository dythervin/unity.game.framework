namespace Dythervin.Game.Framework
{
    public interface ICommandProcessor
    {
        bool IsEnabled { get; set; }

        void Process();

        void Clear();
    }
}