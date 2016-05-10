using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Day7
{
    public sealed partial class CircuitBoard : ICircuitBoard
    {
        private readonly Dictionary<string, IWire> _wires = new Dictionary<string, IWire>();

        public IWire this[string identifier]
        {
            get
            {
                CheckIfIdentifierIsCorrect(identifier);
                AssureThatAWireExists(identifier);
                Debug.Assert(_wires.ContainsKey(identifier));
                return _wires[identifier];
            }
        }

        private void AssureThatAWireExists(string identifier)
        {
            Debug.Assert(identifier != null);

            if (!_wires.ContainsKey(identifier))
            {
                _wires.Add(identifier, new Wire(null, identifier));
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

        public void ProcessCommand(string commandStr)
        {
            if (commandStr == null)
                throw new ArgumentNullException(nameof(commandStr));

            Command command = ParseCommand(commandStr);
            command.Execute();
        }

        public void ProcessCommands(IEnumerable<string> commands)
        {
            if (commands == null)
                throw new ArgumentNullException(nameof(commands));
            if (commands.Any(str => str == null))
                throw new ArgumentException("One of commands is null.", nameof(commands));

            commands.ForEach(ProcessCommand);
        }

        private Command ParseCommand(string commandStr)
        {
            Debug.Assert(commandStr != null);

            commandStr = commandStr.Trim();
            string[] splitted = commandStr.SplitAndRemoveEmptyEntries("->");
            if (splitted.Length == 2)
            {
                Command leftOperand = ParseCommand(splitted[0]);
                Command rightOperand = ParseCommand(splitted[1]);
                return new AssignCommand(this, leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("LSHIFT");
            if (splitted.Length == 2)
            {
                Command leftOperand = ParseCommand(splitted[0]);
                Command rightOperand = ParseCommand(splitted[1]);
                return new LShiftCommand(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("RSHIFT");
            if (splitted.Length == 2)
            {
                Command leftOperand = ParseCommand(splitted[0]);
                Command rightOperand = ParseCommand(splitted[1]);
                return new RShiftCommand(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("AND");
            if (splitted.Length == 2)
            {
                Command leftOperand = ParseCommand(splitted[0]);
                Command rightOperand = ParseCommand(splitted[1]);
                return new AndCommand(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("OR");
            if (splitted.Length == 2)
            {
                Command leftOperand = ParseCommand(splitted[0]);
                Command rightOperand = ParseCommand(splitted[1]);
                return new OrCommand(leftOperand, rightOperand);
            }

            splitted = commandStr.SplitAndRemoveEmptyEntries("NOT");
            if (commandStr.StartsWith("NOT ") && splitted.Length == 1)
            {
                Command operand = ParseCommand(splitted[0]);
                return new NotCommand(operand);
            }

            ushort signalValue;
            if (ushort.TryParse(commandStr, out signalValue))
                return new ValueCommand(signalValue);

            return new WireCommand(this, commandStr);
        }

        private void SetWireValue(string wireIdentifier, ushort? signalValue)
        {
            Debug.Assert(wireIdentifier != null);

            AssureThatAWireExists(wireIdentifier);
            _wires[wireIdentifier].Signal = signalValue;
        }

        public IEnumerator<IWire> GetEnumerator() => _wires.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class Wire : IWire
        {
            public Wire(ushort? signal, string identifier)
            {
                if (identifier == null)
                    throw new ArgumentNullException(nameof(identifier));

                Signal = signal;
                Identifier = identifier;
            }

            public string Identifier { get; }

            private ushort? _signal;
            public ushort? Signal
            {
                get { return _signal; }
                set
                {
                    if (Signal != value)
                    {
                        _signal = value;
                        SignalValueChanged();
                    }
                }
            }

            private EventHandler<EventArgs> _onSignalValueChanged;
            public event EventHandler<EventArgs> OnSignalValueChanged
            {
                add { _onSignalValueChanged += value; }
                remove { _onSignalValueChanged -= value; }
            }

            private void SignalValueChanged()
            {
                EventHandler<EventArgs> onSignalValueChanged = _onSignalValueChanged;
                if (onSignalValueChanged != null)
                {
                    onSignalValueChanged(this, EventArgs.Empty);
                }
            }
        }
    }
}