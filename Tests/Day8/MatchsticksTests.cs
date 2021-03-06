﻿using NUnit.Framework;
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

        [Test]
        public void Matchsticks_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day8.Matchsticks_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);

            IStringParser stringParser = Kernel.Get<IStringParser>();
            IComposedStringParseResult result = stringParser.Parse(mySplittedPuzzleInput);
            Console.WriteLine($"Answer = {result.CharactersOfCode - result.Length}");
        }

        // "" encodes to "\"\"", an increase from 2 characters to 6.
        [TestCase("\"\"", ExpectedResult = 6)]
        // "abc" encodes to "\"abc\"", an increase from 5 characters to 9.
        [TestCase("\"abc\"", ExpectedResult = 9)]
        // "aaa\"aaa" encodes to "\"aaa\\\"aaa\"", an increase from 10 characters to 16.
        [TestCase("\"aaa\\\"aaa\"", ExpectedResult = 16)]
        // "\x27" encodes to "\"\\x27\"", an increase from 6 characters to 11.
        [TestCase("\"\\x27\"", ExpectedResult = 11)]
        public int Encoded_string_length_is_correctly_computed(string input)
        {
            IStringParser stringParser = Kernel.Get<IStringParser>();
            IStringParseResult result = stringParser.Parse(input);
            return result.EncodedLength;
        }

        [Test]
        public void Matchsticks_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day8.Matchsticks_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);

            IStringParser stringParser = Kernel.Get<IStringParser>();
            IComposedStringParseResult result = stringParser.Parse(mySplittedPuzzleInput);
            Console.WriteLine($"Answer = {result.EncodedLength - result.CharactersOfCode}");
        }
    }
}