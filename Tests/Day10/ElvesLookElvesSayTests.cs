using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day10
{
    [TestFixture]
    public sealed class ElvesLookElvesSayTests : TestBase
    {
        // 1 becomes 11 (1 copy of digit 1).
        [TestCase("1", ExpectedResult = "11")]
        // 11 becomes 21 (2 copies of digit 1).
        [TestCase("11", ExpectedResult = "21")]
        // 21 becomes 1211 (one 2 followed by one 1).
        [TestCase("21", ExpectedResult = "1211")]
        // 1211 becomes 111221 (one 1, one 2, and two 1s).
        [TestCase("1211", ExpectedResult = "111221")]
        // 111221 becomes 312211 (three 1s, two 2s, and one 1).
        [TestCase("111221", ExpectedResult = "312211")]
        public string Provided_test_cases_are_solved_correctly(string input)
        {
            ILookAndSayProcessor lookAndSayProcessor = Kernel.Get<ILookAndSayProcessor>();
            string result = lookAndSayProcessor.Process(input);
            return result;
        }

        [Test]
        public void ElvesLookElvesSay_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day10.ElvesLookElvesSay_PuzzleInput.txt");

            ILookAndSayProcessor lookAndSayProcessor = Kernel.Get<ILookAndSayProcessor>();
            string result = lookAndSayProcessor.ProcessIteratively(myPuzzleInput, 40);

            Console.WriteLine($"Answer = {result.Length}");
        }
    }
}