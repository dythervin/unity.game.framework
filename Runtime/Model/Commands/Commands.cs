using System;
using System.Collections.Generic;

namespace Dythervin.Game.Framework
{
    public class Commands : ICommands
    {
        private readonly Dictionary<Type, ICommandProcessor> _commandProcessors =
            new Dictionary<Type, ICommandProcessor>();

        private readonly Queue<ICommandProcessor> _queue = new Queue<ICommandProcessor>();

        public void Register<T>(ICommandProcessor<T> processor)
            where T : struct, ICommand
        {
            _commandProcessors.Add(typeof(T), processor);
        }

        public void Enqueue<T>(T command)
            where T : struct, ICommand
        {
            if (!_commandProcessors.TryGetValue(typeof(T), out ICommandProcessor processor))
                throw new KeyNotFoundException($"No command processor for {typeof(T)}");

            var processorT = (ICommandProcessor<T>)processor;
            processorT.Enqueue(command);
            _queue.Enqueue(processorT);
        }

        public void RunAll()
        {
            Run(_queue.Count);
        }

        public void Run(int count)
        {
            for (; count > 0; count--)
            {
                _queue.Dequeue().Process();
            }
        }

        public void Clear()
        {
            foreach (ICommandProcessor processor in _commandProcessors.Values)
            {
                processor.Clear();
            }

            _queue.Clear();
        }
    }
}