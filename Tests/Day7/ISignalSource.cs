using System;

namespace Tests.Day7
{
    public interface ISignalSource
    {
        ushort? Signal { get; }
        event EventHandler<EventArgs> OnSignalChanged;
    }
}