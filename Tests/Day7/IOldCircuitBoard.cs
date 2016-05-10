using System.Collections.Generic;

namespace Tests.Day7
{
    public interface IOldCircuitBoard : IEnumerable<IOldWire>
    {
        void ProcessCommand(string command);
        void ProcessCommands(IEnumerable<string> commands);
        IOldWire this[string identifier] { get; }
    }
}