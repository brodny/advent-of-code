using System.Collections.Generic;

namespace Tests.Day6
{
    public interface ILightsGridController
    {
        void ProcessCommand(string input);
        void ProcessCommands(IEnumerable<string> inputs);
    }
}