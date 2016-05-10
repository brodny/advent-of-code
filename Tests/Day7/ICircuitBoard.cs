using System.Collections.Generic;

namespace Tests.Day7
{
    public interface ICircuitBoard
    {
        void ProcessCommand(string commandStr);
        void ProcessCommands(IEnumerable<string> commands);
        Wire this[string wireIdentifier] { get; }
        IEnumerable<Wire> Wires { get; }
    }
}