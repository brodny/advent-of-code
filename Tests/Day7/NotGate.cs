namespace Tests.Day7
{
    public sealed class NotGate : BaseGate, ISignalSource
    {
        public NotGate(ISignalSource input1)
            : base(input1)
        { }

        protected override int? ComputeOutputValue() => ~Input1.Signal.Value;
    }
}