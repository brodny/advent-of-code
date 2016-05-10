namespace Tests.Day7
{
    public sealed class ConstantSignalSource : SignalSourceBase, ISignalSource
    {
        public ConstantSignalSource(ushort? signal)
        {
            SetSignal(signal);
        }

        public void SetSignal(ushort? signal) => SetOutputSignal(signal);
    }
}