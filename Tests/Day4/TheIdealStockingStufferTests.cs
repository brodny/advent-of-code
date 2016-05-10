using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day4
{
    [TestFixture]
    public sealed class TheIdealStockingStufferTests : TestBase
    {
        // If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043
        // starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
        [TestCase("abcdef", ExpectedResult = 609043)]
        // If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash
        // starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....
        [TestCase("pqrstuv", ExpectedResult = 1048970)]
        public int The_ideal_stocking_stuffer_returns_specified_example_values(string secretKey)
        {
            ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder lowestNumberForMd5WithAtLeastFindLeadingZerosFinder =
                Kernel.Get<ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder>();
            int lowestNumber = lowestNumberForMd5WithAtLeastFindLeadingZerosFinder.FindLowestNumber(secretKey, 5);
            return lowestNumber;
        }

        [Test]
        public void TheIdealStockingStuffer_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day4.TheIdealStockingStuffer_PuzzleInput.txt");
            ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder lowestNumberForMd5WithAtLeastFindLeadingZerosFinder =
                Kernel.Get<ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder>();
            int lowestNumber = lowestNumberForMd5WithAtLeastFindLeadingZerosFinder.FindLowestNumber(myPuzzleInput, 5);
            Console.WriteLine($"Answer = {lowestNumber}");
        }

        [Test]
        public void TheIdealStockingStuffer_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day4.TheIdealStockingStuffer_PuzzleInput.txt");
            ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder lowestNumberForMd5WithAtLeastFindLeadingZerosFinder =
                Kernel.Get<ILowestNumberForMd5WithAtLeastFiveLeadingZerosFinder>();
            int lowestNumber = lowestNumberForMd5WithAtLeastFindLeadingZerosFinder.FindLowestNumber(myPuzzleInput, 6);
            Console.WriteLine($"Answer = {lowestNumber}");
        }
    }
}