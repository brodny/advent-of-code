using System.Collections.Generic;

namespace Tests.Day9
{
    public interface IPermutationGenerator
    {
        IEnumerable<IEnumerable<string>> Generate(IEnumerable<string> elements);
    }
}