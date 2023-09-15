namespace Dythervin.Game.Framework
{
    public interface ICommands
    {
        void Register<T>(ICommandProcessor<T> processor)
            where T : struct, ICommand;

        void Enqueue<T>(T command)
            where T : struct, ICommand;

        void RunAll();

        void Run(int count);

        void Clear();
    }
}