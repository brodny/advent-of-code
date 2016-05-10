using System.Collections.Generic;

namespace Tests.Day5
{
    public interface INaughtyOrNiceStringsCounter
    {
        int CountStrings(IEnumerable<string> strings, StringType stringType);
    }
}