using System.Collections.Generic;

namespace Tests.Day8
{
    public interface IComposedStringParseResult : IStringParseResult, IEnumerable<IStringParseResult>
    {
    }
}