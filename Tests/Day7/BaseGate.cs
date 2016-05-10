using System;

namespace Tests.Day7
{
    public abstract class BaseGate : SignalSourceBase, ISignalSource
    {
        public BaseGate(ISignalSource input1)
        {
            Input1 = input1;
        }

        private ISignalSource _input1;
        public ISignalSource Input1
        {
            get { return _input1; }
            set { SetInput(value, ref _input1); }
        }

        protected void SetInput(ISignalSource value, ref ISignalSource backingField)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (ReferenceEquals(value, backingField))
                return;

            if (backingField != null)
            {
                backingField.OnSignalChanged -= Input_OnSignalChanged;
            }

            backingField = value;
            backingField.OnSignalChanged += Input_OnSignalChanged;
            InputSignalChanged();
        }

        private void Input_OnSignalChanged(object sender, EventArgs e)
        {
            InputSignalChanged();
        }

        private void InputSignalChanged()
        {
            if (AllInputsHaveValue())
            {
                SetOutputSignal(ComputeOutputValue());
            }
        }

        protected virtual bool AllInputsHaveValue() => Input1 != null && Input1.Signal.HasValue;
        protected abstract int? ComputeOutputValue();
    }
}