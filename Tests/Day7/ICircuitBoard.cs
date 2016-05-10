using System.Collections.Generic;

namespace Tests.Day7
{
    public interface ICircuitBoard
    {
        void ProcessCommand(string commandStr);
        void ProcessCommands(IEnumerable<string> commands);
        ISignalSource this[string wireIdentifier] { get; }
    }
}