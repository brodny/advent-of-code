namespace Tests.Day7
{
    public abstract class TwoInputsGate : BaseGate, ISignalSource
    {
        public TwoInputsGate(ISignalSource input1, ISignalSource input2)
            : base(input1)
        {
            Input2 = input2;
        }

        private ISignalSource _input2;
        public ISignalSource Input2
        {
            get { return _input2; }
            set { SetInput(value, ref _input2); }
        }

        protected override bool AllInputsHaveValue() => base.AllInputsHaveValue() && Input2 != null && Input2.Signal.HasValue;
    }
}