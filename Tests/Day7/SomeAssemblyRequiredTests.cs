using Ninject;
using NUnit.Framework;
using System;

namespace Tests.Day7
{
    [TestFixture]
    public sealed class SomeAssemblyRequiredTests : TestBase
    {
        // 123 -> x means that the signal 123 is provided to wire x.
        [Test]
        public void Assignment_to_a_wire_results_in_wire_state_change()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("123 -> x");
            ushort? wireXFinalSignal = circuitBoard["x"].Signal;
            Assert.AreEqual(123, wireXFinalSignal);
        }

        // x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
        [Test]
        public void AND_command_is_recognized()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("10 -> x");
            circuitBoard.ProcessCommand("7 -> y");
            circuitBoard.ProcessCommand("x AND y -> z");
            ushort? wireZFinalSignal = circuitBoard["z"].Signal;
            Assert.AreEqual(2, wireZFinalSignal);
        }

        // x OR y -> z means that the bitwise OR of wire x and wire y is provided to wire z.
        [Test]
        public void OR_command_is_recognized()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("10 -> x");
            circuitBoard.ProcessCommand("7 -> y");
            circuitBoard.ProcessCommand("x OR y -> z");
            ushort? wireZFinalSignal = circuitBoard["z"].Signal;
            Assert.AreEqual(15, wireZFinalSignal);
        }

        // p LSHIFT 2 -> q means that the value from wire p is left-shifted by 2 and then provided to wire q.
        [Test]
        public void LSHIFT_command_is_recognized()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("10 -> p");
            circuitBoard.ProcessCommand("p LSHIFT 2 -> q");
            ushort? wireQFinalSignal = circuitBoard["q"].Signal;
            Assert.AreEqual(40, wireQFinalSignal);
        }

        // p RSHIFT 2 -> q means that the value from wire p is right-shifted by 2 and then provided to wire q.
        [Test]
        public void RSHIFT_command_is_recognized()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("10 -> p");
            circuitBoard.ProcessCommand("p RSHIFT 2 -> q");
            ushort? wireQFinalSignal = circuitBoard["q"].Signal;
            Assert.AreEqual(2, wireQFinalSignal);
        }

        // NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
        [Test]
        public void NOT_command_is_recognized()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("10 -> e");
            circuitBoard.ProcessCommand("NOT e -> f");
            ushort? wireFFinalSignal = circuitBoard["f"].Signal;
            Assert.AreEqual(65525, wireFFinalSignal);
        }

        //For example, here is a simple circuit:
        //123 -> x
        //456 -> y
        //x AND y -> d
        //x OR y -> e
        //x LSHIFT 2 -> f
        //y RSHIFT 2 -> g
        //NOT x -> h
        //NOT y -> i

        //After it is run, these are the signals on the wires:
        //d: 72
        //e: 507
        //f: 492
        //g: 114
        //h: 65412
        //i: 65079
        //x: 123
        //y: 456
        [Test]
        public void Sample_circuit_is_calculated_correctly()
        {
            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommand("123 -> x");
            circuitBoard.ProcessCommand("456 -> y");
            circuitBoard.ProcessCommand("x AND y -> d");
            circuitBoard.ProcessCommand("x OR y -> e");
            circuitBoard.ProcessCommand("x LSHIFT 2 -> f");
            circuitBoard.ProcessCommand("y RSHIFT 2 -> g");
            circuitBoard.ProcessCommand("NOT x -> h");
            circuitBoard.ProcessCommand("NOT y -> i");

            ushort? wireDFinalSignal = circuitBoard["d"].Signal;
            ushort? wireEFinalSignal = circuitBoard["e"].Signal;
            ushort? wireFFinalSignal = circuitBoard["f"].Signal;
            ushort? wireGFinalSignal = circuitBoard["g"].Signal;
            ushort? wireHFinalSignal = circuitBoard["h"].Signal;
            ushort? wireIFinalSignal = circuitBoard["i"].Signal;
            ushort? wireXFinalSignal = circuitBoard["x"].Signal;
            ushort? wireYFinalSignal = circuitBoard["y"].Signal;

            Assert.AreEqual(72, wireDFinalSignal);
            Assert.AreEqual(507, wireEFinalSignal);
            Assert.AreEqual(492, wireFFinalSignal);
            Assert.AreEqual(114, wireGFinalSignal);
            Assert.AreEqual(65412, wireHFinalSignal);
            Assert.AreEqual(65079, wireIFinalSignal);
            Assert.AreEqual(123, wireXFinalSignal);
            Assert.AreEqual(456, wireYFinalSignal);
        }

        [Test]
        public void SomeAssemblyRequired_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day7.SomeAssemblyRequired_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);

            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommands(mySplittedPuzzleInput);
            ushort? wireAFinalSignal = circuitBoard["a"].Signal;
            Console.WriteLine($"Answer = {wireAFinalSignal}");
        }

        [Test]
        public void SomeAssemblyRequired_part_two_Get_answer()
        {
            string myPuzzleInput = Utils.GetTextFromResource("Tests.Day7.SomeAssemblyRequired_PuzzleInput.txt");
            string[] mySplittedPuzzleInput = Utils.SplitLines(myPuzzleInput);

            ICircuitBoard circuitBoard = Kernel.Get<ICircuitBoard>();
            circuitBoard.ProcessCommands(mySplittedPuzzleInput);
            ushort? wireASignal = circuitBoard["a"].Signal;
            foreach (IWire wire in circuitBoard)
            {
                wire.Signal = null;
            }
            circuitBoard["b"].Signal = wireASignal;
            ushort? wireAFinalSignal = circuitBoard["a"].Signal;
            Console.WriteLine($"Answer = {wireAFinalSignal}");
        }
    }
}