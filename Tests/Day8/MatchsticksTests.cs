using NUnit.Framework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Day8
{
    [TestFixture]
    public sealed class MatchsticksTests : TestBase
    {
        // "" is 2 characters of code (the two double quotes), but the string contains zero characters.
        [TestCase("\"\"", ExpectedResult = 2)]
        // "abc" is 5 characters of code, but 3 characters in the string data.
        [TestCase("\"abc\"", ExpectedResult = 5)]
        // "aaa\"aaa" is 10 characters of code, but the string itself contains six "a" characters
        // and a single, escaped quote character, for a total of 7 characters in the string data.
        [TestCase("\"aaa\\\"aaa\"", ExpectedResult = 10)]
        // "\x27" is 6 characters of code, but the string itself contains just one - an apostrophe ('),
        // escaped using hexadecimal notation.
        [TestCase("\"\\x27\"", ExpectedResult = 6)]
        public int Characters_of_code_are_correctly_computed(string input)
        {
            IStringParser stringParser = Kernel.Get<IStringParser>();
            IStringParseResult result = stringParser.Parse(input);
            return result.CharactersOfCode;
        }

        // "" is 2 characters of code (the two double quotes), but the string contains zero characters.
        [TestCase("\"\"", ExpectedResult = 0)]
        // "abc" is 5 characters of code, but 3 characters in the string data.
        [TestCase("\"abc\"", ExpectedResult = 3)]
        // "aaa\"aaa" is 10 characters of code, but the string itself contains six "a" characters
        // and a single, escaped quote character, for a total of 7 characters in the string data.
        [TestCase("\"aaa\\\"aaa\"", ExpectedResult = 7)]
        // "\x27" is 6 characters of code, but the string itself contains just one - an apostrophe ('),
        // escaped using hexadecimal notation.
        [TestCase("\"\\x27\"", ExpectedResult = 1)]
        public int String_length_is_correctly_computed(string input)
        {
            IStringParser stringParser = Kernel.Get<IStringParser>();
            IStringParseResult result = stringParser.Parse(input);
            return result.Length;
        }
    }
}