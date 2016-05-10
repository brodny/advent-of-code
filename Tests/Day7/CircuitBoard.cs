using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day7
{
    public sealed class CircuitBoard : ICircuitBoard
    {
        public void ProcessCommand(string commandStr)
        {
            if (commandStr == null)
                throw new ArgumentNullException(nameof(commandStr));

            ParseCommand(commandStr);
        }

        public void ProcessCommands(IEnumerable<string> commands)
        {
            if (commands == null)
                throw new ArgumentNullException(nameof(commands));
            if (commands.Any(str => str == null))
                throw new ArgumentException("One of commands is null.", nameof(commands));

            commands.ForEach(ProcessCommand);
        }

        private ISignalSource ParseCommand(string commandStr)
        {
            Debug.Assert(commandStr != null);

            commandStr = commandStr.Trim();
            string[] splitted = commandStr.SplitAndRemoveEmptyEntries("->");
            if (splitted.Length == 2)
            {
                ISignalSource leftOperand = ParseCommand(splitted[0]);
                Wire wire = GetWire(splitted[1].Trim());
                wire.Input1 = leftOperand;
                return wire;
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("LSHIFT");
            if (splitted.Length == 2)
            {
                ISignalSource leftOperand = ParseCommand(splitted[0]);
                ISignalSource rightOperand = ParseCommand(splitted[1]);
                return new LShiftGate(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("RSHIFT");
            if (splitted.Length == 2)
            {
                ISignalSource leftOperand = ParseCommand(splitted[0]);
                ISignalSource rightOperand = ParseCommand(splitted[1]);
                return new RShiftGate(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("AND");
            if (splitted.Length == 2)
            {
                ISignalSource leftOperand = ParseCommand(splitted[0]);
                ISignalSource rightOperand = ParseCommand(splitted[1]);
                return new AndGate(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("OR");
            if (splitted.Length == 2)
            {
                ISignalSource leftOperand = ParseCommand(splitted[0]);
                ISignalSource rightOperand = ParseCommand(splitted[1]);
                return new OrGate(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("NOT");
            if (commandStr.StartsWith("NOT ") && splitted.Length == 1)
            {
                ISignalSource operand = ParseCommand(splitted[0]);
                return new NotGate(operand);
            }

            ushort signalValue;
            if (ushort.TryParse(commandStr, out signalValue))
                return new ConstantSignalSource(signalValue);

            return GetWire(commandStr);
        }

        public ISignalSource this[string wireIdentifier]
        {
            get
            {
                CheckIfIdentifierIsCorrect(wireIdentifier);
                AssureThatAWireExists(wireIdentifier);
                Debug.Assert(_wires.ContainsKey(wireIdentifier));
                return _wires[wireIdentifier];
            }
        }

        private readonly Dictionary<string, Wire> _wires = new Dictionary<string, Wire>();

        private Wire GetWire(string identifier)
        {
            CheckIfIdentifierIsCorrect(identifier);
            AssureThatAWireExists(identifier);
            Debug.Assert(_wires.ContainsKey(identifier));
            return _wires[identifier];
        }

        private void AssureThatAWireExists(string identifier)
        {
            Debug.Assert(identifier != null);

            if (!_wires.ContainsKey(identifier))
            {
                _wires.Add(identifier, new Wire(new ConstantSignalSource(null)));
            }
        }

        private void CheckIfIdentifierIsCorrect(string identifier)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier), "identifier cannot be null.");
            if (!IsAllLower(identifier))
                throw new ArgumentException("identifier must contain only lowercase letters.", nameof(identifier));
        }

        private bool IsAllLower(string identifier)
        {
            Debug.Assert(identifier != null);

            for (int i = 0; i < identifier.Length; ++i)
            {
                if (!char.IsLower(identifier[i]))
                    return false;
            }

            return true;
        }
    }
}