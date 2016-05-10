using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Day5
{
    public sealed class NaughtyOrNiceStringsCounter : INaughtyOrNiceStringsCounter
    {
        private readonly INaughtyOrNiceStringRecognizer _naughtyOrNiceStringRecognizer;

        public NaughtyOrNiceStringsCounter(INaughtyOrNiceStringRecognizer naughtyOrNiceStringRecognizer)
        {
            if (naughtyOrNiceStringRecognizer == null)
                throw new ArgumentNullException(nameof(naughtyOrNiceStringRecognizer));

            _naughtyOrNiceStringRecognizer = naughtyOrNiceStringRecognizer;
        }

        public int CountStrings(IEnumerable<string> strings, StringType stringType)
        {
            if (strings == null)
                throw new ArgumentNullException(nameof(strings));

            IEnumerable<string> stringsWithSpecifiedType = strings
                .Where(@string => _naughtyOrNiceStringRecognizer.RecognizeStringType(@string) == stringType);
            int count = stringsWithSpecifiedType.Count();
            return count;
        }
    }
}