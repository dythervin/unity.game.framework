using System.Collections.Generic;
using Dythervin.Core;

namespace Dythervin.Game.Framework
{
    public interface ICommandProcessor<in T> : ICommandProcessor
        where T : struct, ICommand
    {
        void Enqueue(T command);
    }

    public abstract class CommandProcessor<T> : ICommandProcessor<T>
        where T : struct, ICommand
    {
        private readonly Queue<T> _commands = new Queue<T>();

        public bool IsEnabled { get; set; } = true;

        public void Enqueue(T command)
        {
            _commands.Enqueue(command);
        }

        public void Process()
        {
            if (!IsEnabled)
            {
                DDebug.LogWarning("CommandProcessor is not enabled!");
                return;
            }

            Process(_commands.Dequeue());
        }

        public void Clear()
        {
            _commands.Clear();
        }

        protected abstract void Process(T command);
    }
}