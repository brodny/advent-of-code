using System.Collections.Generic;

namespace Tests.Day7
{
    public interface ICircuitBoard : IEnumerable<IWire>
    {
        void ProcessCommand(string command);
        void ProcessCommands(IEnumerable<string> commands);
        IWire this[string identifier] { get; }
    }
}