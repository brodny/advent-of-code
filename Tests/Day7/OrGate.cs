namespace Tests.Day7
{
    public sealed class OrGate : TwoInputsGate, ISignalSource
    {
        public OrGate(ISignalSource input1, ISignalSource input2)
            : base(input1, input2)
        { }

        protected override int? ComputeOutputValue() => Input1.Signal.Value | Input2.Signal.Value;
    }
}