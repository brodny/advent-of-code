namespace Tests.Day7
{
    public sealed class Wire : BaseGate, ISignalSource
    {
        public Wire(ISignalSource input1)
            : base(input1)
        { }

        protected override int? ComputeOutputValue() => Input1.Signal;
    }
}