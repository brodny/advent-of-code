using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Day7
{
    public class SignalSourceBase : ISignalSource
    {
        public ushort? Signal { get; private set; }
        public event EventHandler<EventArgs> OnSignalChanged;

        protected void SignalChanged()
        {
            EventHandler<EventArgs> signalChanged = OnSignalChanged;
            if (signalChanged != null)
            {
                signalChanged(this, EventArgs.Empty);
            }
        }

        protected void SetOutputSignal(int? signal)
        {
            ushort? newSignal = (ushort?)signal;
            if (newSignal != Signal)
            {
                Signal = newSignal;
                SignalChanged();
            }
        }
    }
}