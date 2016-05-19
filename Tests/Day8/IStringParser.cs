using System.Collections.Generic;

namespace Tests.Day8
{
    public interface IStringParser
    {
        IStringParseResult Parse(string input);
        IComposedStringParseResult Parse(IEnumerable<string> input);
    }
}