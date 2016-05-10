using System;

namespace Tests.Day7
{
    public sealed partial class CircuitBoard
    {
        private abstract class Command
        {
            public abstract object Execute();

            private EventHandler<EventArgs> _onSignalValueChanged;
            public event EventHandler<EventArgs> OnSignalValueChanged
            {
                add { _onSignalValueChanged += value; }
                remove { _onSignalValueChanged -= value; }
            }

            protected void SignalValueChanged()
            {
                EventHandler<EventArgs> onSignalValueChanged = _onSignalValueChanged;
                if (onSignalValueChanged != null)
                {
                    onSignalValueChanged(this, EventArgs.Empty);
                }
            }

            protected void Observe(Command otherCommand)
            {
                if (otherCommand == null)
                    throw new ArgumentNullException(nameof(otherCommand));

                otherCommand.OnSignalValueChanged += OtherCommand_OnSignalValueChanged;
            }

            protected virtual void OtherCommand_OnSignalValueChanged(object sender, EventArgs e)
            {
                SignalValueChanged();
            }
        }

        private class AssignCommand : Command
        {
            private readonly CircuitBoard _circuitBoard;
            private readonly Command _leftOperand;
            private readonly Command _rightOperand;

            public AssignCommand(CircuitBoard circuitBoard, Command leftOperand, Command rightOperand)
            {
                if (circuitBoard == null)
                    throw new ArgumentNullException(nameof(circuitBoard));
                if (leftOperand == null)
                    throw new ArgumentNullException(nameof(leftOperand));
                if (rightOperand == null)
                    throw new ArgumentNullException(nameof(rightOperand));

                _circuitBoard = circuitBoard;
                _leftOperand = leftOperand;
                _rightOperand = rightOperand;
                Observe(_leftOperand);
                Observe(_rightOperand);
            }

            public override object Execute()
            {
                string wireIdentifier = ((WireCommand)_rightOperand).WireIdentifier;
                ushort? signalValue = (ushort?)_leftOperand.Execute();
                _circuitBoard.SetWireValue(wireIdentifier, signalValue);
                return null;
            }

            protected override void OtherCommand_OnSignalValueChanged(object sender, EventArgs e)
            {
                base.OtherCommand_OnSignalValueChanged(sender, e);
                Execute();
            }
        }

        private class ValueCommand : Command
        {
            private readonly ushort _signalValue;

            public ValueCommand(ushort signalValue)
            {
                _signalValue = signalValue;
            }

            public override object Execute() => _signalValue;
        }

        private class WireCommand : Command
        {
            private readonly CircuitBoard _circuitBoard;
            private readonly string _wireIdentifier;

            public WireCommand(CircuitBoard circuitBoard, string wireIdentifier)
            {
                if (circuitBoard == null)
                    throw new ArgumentNullException(nameof(circuitBoard));
                if (wireIdentifier == null)
                    throw new ArgumentNullException(nameof(wireIdentifier));
                circuitBoard.CheckIfIdentifierIsCorrect(wireIdentifier);

                _circuitBoard = circuitBoard;
                _wireIdentifier = wireIdentifier;
                _circuitBoard[_wireIdentifier].OnSignalValueChanged += WireCommand_OnSignalValueChanged;
            }

            private void WireCommand_OnSignalValueChanged(object sender, EventArgs e)
            {
                Execute();
                SignalValueChanged();
            }

            public override object Execute() => _circuitBoard[_wireIdentifier].Signal;
            public string WireIdentifier => _wireIdentifier;
        }

        private class AndCommand : Command
        {
            private readonly Command _leftOperand;
            private readonly Command _rightOperand;

            public AndCommand(Command leftOperand, Command rightOperand)
            {
                if (leftOperand == null)
                    throw new ArgumentNullException(nameof(leftOperand));
                if (rightOperand == null)
                    throw new ArgumentNullException(nameof(rightOperand));

                _leftOperand = leftOperand;
                _rightOperand = rightOperand;
                Observe(_leftOperand);
                Observe(_rightOperand);
            }

            public override object Execute()
            {
                ushort? leftOperand = (ushort?)_leftOperand.Execute();
                ushort? rightOperand = (ushort?)_rightOperand.Execute();
                return (ushort?)(leftOperand & rightOperand);
            }
        }

        private class OrCommand : Command
        {
            private readonly Command _leftOperand;
            private readonly Command _rightOperand;

            public OrCommand(Command leftOperand, Command rightOperand)
            {
                if (leftOperand == null)
                    throw new ArgumentNullException(nameof(leftOperand));
                if (rightOperand == null)
                    throw new ArgumentNullException(nameof(rightOperand));

                _leftOperand = leftOperand;
                _rightOperand = rightOperand;
                Observe(_leftOperand);
                Observe(_rightOperand);
            }

            public override object Execute()
            {
                ushort? leftOperand = (ushort?)_leftOperand.Execute();
                ushort? rightOperand = (ushort?)_rightOperand.Execute();
                return (ushort?)(leftOperand | rightOperand);
            }
        }

        private class LShiftCommand : Command
        {
            private readonly Command _leftOperand;
            private readonly Command _rightOperand;

            public LShiftCommand(Command leftOperand, Command rightOperand)
            {
                if (leftOperand == null)
                    throw new ArgumentNullException(nameof(leftOperand));
                if (rightOperand == null)
                    throw new ArgumentNullException(nameof(rightOperand));

                _leftOperand = leftOperand;
                _rightOperand = rightOperand;
                Observe(_leftOperand);
                Observe(_rightOperand);
            }

            public override object Execute()
            {
                ushort? leftOperand = (ushort?)_leftOperand.Execute();
                ushort? rightOperand = (ushort?)_rightOperand.Execute();
                return (ushort?)(leftOperand << rightOperand);
            }
        }

        private class RShiftCommand : Command
        {
            private readonly Command _leftOperand;
            private readonly Command _rightOperand;

            public RShiftCommand(Command leftOperand, Command rightOperand)
            {
                if (leftOperand == null)
                    throw new ArgumentNullException(nameof(leftOperand));
                if (rightOperand == null)
                    throw new ArgumentNullException(nameof(rightOperand));

                _leftOperand = leftOperand;
                _rightOperand = rightOperand;
                Observe(_leftOperand);
                Observe(_rightOperand);
            }

            public override object Execute()
            {
                ushort? leftOperand = (ushort?)_leftOperand.Execute();
                ushort? rightOperand = (ushort?)_rightOperand.Execute();
                return (ushort?)(leftOperand >> rightOperand);
            }
        }

        private class NotCommand : Command
        {
            private readonly Command _operand;

            public NotCommand(Command operand)
            {
                if (operand == null)
                    throw new ArgumentNullException(nameof(operand));

                _operand = operand;
                Observe(_operand);
            }

            public override object Execute()
            {
                ushort? operand = (ushort?)_operand.Execute();
                return (ushort?)(~operand);
            }
        }
    }
}