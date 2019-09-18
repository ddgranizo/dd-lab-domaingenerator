using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Events
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string log)
        {
            Log = log;
        }

        public string Log { get; }
    }
}
