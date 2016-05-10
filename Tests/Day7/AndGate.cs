namespace Tests.Day7
{
    public sealed class AndGate : TwoInputsGate, ISignalSource
    {
        public AndGate(ISignalSource input1, ISignalSource input2)
            : base(input1, input2)
        { }

        protected override int? ComputeOutputValue() => Input1.Signal.Value & Input2.Signal.Value;
    }
}