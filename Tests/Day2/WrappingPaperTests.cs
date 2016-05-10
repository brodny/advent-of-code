using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Day2
{
    [TestFixture]
    public sealed class WrappingPaperTests : TestBase
    {
        [TestCase("2x3x4", ExpectedResult = 52)]
        // A present with dimensions 2x3x4 requires 2*6 + 2*12 + 2*8 = 52 square feet
        // of wrapping paper plus 6 square feet of slack, for a total of 58 square feet.
        [TestCase("1x1x10", ExpectedResult = 42)]
        // A present with dimensions 1x1x10 requires 2*1 + 2*10 + 2*10 = 42 square feet
        // of wrapping paper plus 1 square foot of slack, for a total of 43 square feet.
        public int Amount_of_wrapping_paper_needed_for_presents_is_calculated_correctly(string input)
        {
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(input);
            return presents.Sum(present => present.TotalArea);
        }

        [TestCase("2x3x4", ExpectedResult = 6)]
        // A present with dimensions 2x3x4 requires 2*6 + 2*12 + 2*8 = 52 square feet
        // of wrapping paper plus 6 square feet of slack, for a total of 58 square feet.
        [TestCase("1x1x10", ExpectedResult = 1)]
        // A present with dimensions 1x1x10 requires 2*1 + 2*10 + 2*10 = 42 square feet
        // of wrapping paper plus 1 square foot of slack, for a total of 43 square feet.
        public int Amount_of_slack_wrapping_paper_needed_for_presents_is_calculated_correctly(string input)
        {
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(input);
            return presents.Sum(present => present.Slack);
        }

        [TestCase("2x3x4", ExpectedResult = 58)]
        // A present with dimensions 2x3x4 requires 2*6 + 2*12 + 2*8 = 52 square feet
        // of wrapping paper plus 6 square feet of slack, for a total of 58 square feet.
        [TestCase("1x1x10", ExpectedResult = 43)]
        // A present with dimensions 1x1x10 requires 2*1 + 2*10 + 2*10 = 42 square feet
        // of wrapping paper plus 1 square foot of slack, for a total of 43 square feet.
        public int Total_amount_of_wrapping_paper_needed_for_presents_is_calculated_correctly(string input)
        {
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(input);
            return presents.Sum(present => present.TotalPaperNeeded);
        }

        [Test]
        public void IWasToldThereWouldBeNoMath_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day2.IWasToldThereWouldBeNoMath_PuzzleInput.txt");
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(myPuzzleInput);
            Console.WriteLine($"Answer = {presents.Sum(present => present.TotalPaperNeeded)}");
        }

        // A present with dimensions 2x3x4 requires 2+2+3+3 = 10 feet of ribbon to wrap the present
        // plus 2*3*4 = 24 feet of ribbon for the bow, for a total of 34 feet.
        [TestCase("2x3x4", ExpectedResult = 10)]
        // A present with dimensions 1x1x10 requires 1+1+1+1 = 4 feet of ribbon to wrap the present
        // plus 1*1*10 = 10 feet of ribbon for the bow, for a total of 14 feet.
        [TestCase("1x1x10", ExpectedResult = 4)]
        public int Amount_of_ribbon_needed_for_presents_is_calculated_correctly(string input)
        {
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(input);
            return presents.Sum(present => present.RibbonNeeded);
        }

        // A present with dimensions 2x3x4 requires 2+2+3+3 = 10 feet of ribbon to wrap the present
        // plus 2*3*4 = 24 feet of ribbon for the bow, for a total of 34 feet.
        [TestCase("2x3x4", ExpectedResult = 24)]
        // A present with dimensions 1x1x10 requires 1+1+1+1 = 4 feet of ribbon to wrap the present
        // plus 1*1*10 = 10 feet of ribbon for the bow, for a total of 14 feet.
        [TestCase("1x1x10", ExpectedResult = 10)]
        public int Amount_of_ribbon_for_the_bow_needed_for_presents_is_calculated_correctly(string input)
        {
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(input);
            return presents.Sum(present => present.RibbonNeededForTheBow);
        }

        // A present with dimensions 2x3x4 requires 2+2+3+3 = 10 feet of ribbon to wrap the present
        // plus 2*3*4 = 24 feet of ribbon for the bow, for a total of 34 feet.
        [TestCase("2x3x4", ExpectedResult = 34)]
        // A present with dimensions 1x1x10 requires 1+1+1+1 = 4 feet of ribbon to wrap the present
        // plus 1*1*10 = 10 feet of ribbon for the bow, for a total of 14 feet.
        [TestCase("1x1x10", ExpectedResult = 14)]
        public int Total_amount_of_ribbon_needed_for_presents_is_calculated_correctly(string input)
        {
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(input);
            return presents.Sum(present => present.TotalRibbonNeeded);
        }

        [Test]
        public void IWasToldThereWouldBeNoMath_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day2.IWasToldThereWouldBeNoMath_PuzzleInput.txt");
            IWrappingPaperParser parser = Kernel.Get<IWrappingPaperParser>();
            IList<Present> presents = parser.Parse(myPuzzleInput);
            Console.WriteLine($"Answer = {presents.Sum(present => present.TotalRibbonNeeded)}");
        }
    }
}