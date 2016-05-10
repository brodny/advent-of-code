using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day1
{
    [TestFixture]
    public sealed class NotQuiteLispTests : TestBase
    {
        [TestCase("(())", ExpectedResult = 0)]
        [TestCase("()()", ExpectedResult = 0)]
        // (()) and ()() both result in floor 0.
        [TestCase("(((", ExpectedResult = 3)]
        [TestCase("(()(()(", ExpectedResult = 3)]
        // ((( and (()(()( both result in floor 3.
        [TestCase("))(((((", ExpectedResult = 3)]
        // ))((((( also results in floor 3.
        [TestCase("())", ExpectedResult = -1)]
        [TestCase("))(", ExpectedResult = -1)]
        // ()) and ))( both result in floor -1 (the first basement level).
        [TestCase(")))", ExpectedResult = -3)]
        [TestCase(")())())", ExpectedResult = -3)]
        // ))) and )())()) both result in floor -3.
        public int Not_quite_Lisp_returns_specified_example_values(string input)
        {
            INotQuiteLisp notQuiteLisp = Kernel.Get<INotQuiteLisp>();
            notQuiteLisp.Process(input);
            int resultFloor = notQuiteLisp.FloorNumber;
            return resultFloor;
        }

        [Test]
        public void Resetting_NotQuiteLisp_object_resets_floor_number()
        {
            INotQuiteLisp notQuiteLisp = Kernel.Get<INotQuiteLisp>();
            notQuiteLisp.Process("(((");
            notQuiteLisp.Reset();
            Assert.AreEqual(0, notQuiteLisp.FloorNumber);
        }

        [Test]
        public void NotQuiteLisp_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day1.NotQuiteLisp_PuzzleInput.txt");
            INotQuiteLisp notQuiteLisp = Kernel.Get<INotQuiteLisp>();
            notQuiteLisp.Process(myPuzzleInput);
            Console.WriteLine($"Answer = {notQuiteLisp.FloorNumber}");
        }

        [TestCase(")", -1, ExpectedResult = 1)]
        // ) causes him to enter the basement at character position 1.
        [TestCase("()())", -1, ExpectedResult = 5)]
        // ()()) causes him to enter the basement at character position 5.
        public int Not_quite_Lisp_position_finder_returns_specified_example_values(string input, int expectedPosition)
        {
            INotQuiteLispPositionFinder notQuiteLispPositionFinder = Kernel.Get<INotQuiteLispPositionFinder>();
            int position = notQuiteLispPositionFinder.FindIndexOfElementThatFirstReachesSpecifiedPosition(input, expectedPosition);
            return position + 1;
        }

        [Test]
        public void NotQuiteLisp_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day1.NotQuiteLisp_PuzzleInput.txt");
            INotQuiteLispPositionFinder notQuiteLispPositionFinder = Kernel.Get<INotQuiteLispPositionFinder>();
            int position = notQuiteLispPositionFinder.FindIndexOfElementThatFirstReachesSpecifiedPosition(myPuzzleInput, -1);
            Console.WriteLine($"Answer = {position + 1}");
        }
    }
}