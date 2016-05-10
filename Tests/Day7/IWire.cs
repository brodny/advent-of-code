using System;

namespace Tests.Day7
{
    public interface IWire
    {
        string Identifier { get; }
        ushort? Signal { get; set; }
        event EventHandler<EventArgs> OnSignalValueChanged;
    }
}