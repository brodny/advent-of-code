using System.Collections.Generic;

namespace Tests.Day2
{
    public interface IWrappingPaperParser
    {
        IList<Present> Parse(string input);
    }
}