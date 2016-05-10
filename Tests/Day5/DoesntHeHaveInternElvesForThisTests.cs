using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day5
{
    [TestFixture]
    public sealed class DoesntHeHaveInternElvesForThisTests : TestBase
    {
        // ugknbfddgicrmopn is nice because it has at least three vowels (u...i...o...),
        // a double letter (...dd...), and none of the disallowed substrings.
        [TestCase("ugknbfddgicrmopn", ExpectedResult = StringType.Nice)]
        // aaa is nice because it has at least three vowels and a double letter,
        // even though the letters used by different rules overlap.
        [TestCase("aaa", ExpectedResult = StringType.Nice)]
        // jchzalrnumimnmhp is naughty because it has no double letter.
        [TestCase("jchzalrnumimnmhp", ExpectedResult = StringType.Naughty)]
        // haegwjzuvuyypxyu is naughty because it contains the string xy.
        [TestCase("haegwjzuvuyypxyu", ExpectedResult = StringType.Naughty)]
        // dvszwmarrgswjxmb is naughty because it contains only one vowel.
        [TestCase("dvszwmarrgswjxmb", ExpectedResult = StringType.Naughty)]
        public StringType Doesnt_he_have_intern_elves_for_this_returns_specified_example_values(string input)
        {
            INaughtyOrNiceStringRecognizer naughtyOrNiceStringRecognizer = Kernel.Get<PartOneNaughtyOrNiceStringRecognizer>();
            StringType type = naughtyOrNiceStringRecognizer.RecognizeStringType(input);
            return type;
        }

        [Test]
        public void DoesntHeHaveInternElvesForThis_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day5.DoesntHeHaveIntern-ElvesForThis_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);
            INaughtyOrNiceStringRecognizer partOneNaughtyOrNiceStringRecognizer = Kernel.Get<PartOneNaughtyOrNiceStringRecognizer>();
            INaughtyOrNiceStringsCounter naughtyOrNiceStringsCounter = new NaughtyOrNiceStringsCounter(partOneNaughtyOrNiceStringRecognizer);
            int count = naughtyOrNiceStringsCounter.CountStrings(mySplittedPuzzleInput, StringType.Nice);
            Console.WriteLine($"Answer = {count}");
        }

        // qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj)
        // and a letter that repeats with exactly one letter between them (zxz).
        [TestCase("qjhvhtzxzqqjkmpb", ExpectedResult = StringType.Nice)]
        // xxyxx is nice because it has a pair that appears twice and a letter that
        // repeats with one between, even though the letters used by each rule overlap.
        [TestCase("xxyxx", ExpectedResult = StringType.Nice)]
        // uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat
        // with a single letter between them.
        [TestCase("uurcxstgmygtbstg", ExpectedResult = StringType.Naughty)]
        // ieodomkazucvgmuy is naughty because it has a repeating letter with one
        // between (odo), but no pair that appears twice.
        [TestCase("ieodomkazucvgmuy", ExpectedResult = StringType.Naughty)]
        public StringType Doesnt_he_have_intern_elves_for_this_part_two_returns_specified_example_values(string input)
        {
            INaughtyOrNiceStringRecognizer naughtyOrNiceStringRecognizer = Kernel.Get<PartTwoNaughtyOrNiceStringRecognizer>();
            StringType type = naughtyOrNiceStringRecognizer.RecognizeStringType(input);
            return type;
        }

        [Test]
        public void DoesntHeHaveInternElvesForThis_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day5.DoesntHeHaveIntern-ElvesForThis_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);
            INaughtyOrNiceStringRecognizer partTwoNaughtyOrNiceStringRecognizer = Kernel.Get<PartTwoNaughtyOrNiceStringRecognizer>();
            INaughtyOrNiceStringsCounter naughtyOrNiceStringsCounter = new NaughtyOrNiceStringsCounter(partTwoNaughtyOrNiceStringRecognizer);
            int count = naughtyOrNiceStringsCounter.CountStrings(mySplittedPuzzleInput, StringType.Nice);
            Console.WriteLine($"Answer = {count}");
        }
    }
}