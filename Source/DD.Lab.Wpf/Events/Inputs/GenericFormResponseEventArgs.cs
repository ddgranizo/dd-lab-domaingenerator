using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class GenericFormResponseEventArgs : EventArgs
    {
        public GenericFormResponseEventArgs(Guid requestId, bool completed, Dictionary<string, object> values)
        {
            RequestId = requestId;
            Completed = completed;
            Values = values;
        }

        public Guid RequestId { get; }
        public bool Completed { get; }
        public Dictionary<string, object> Values { get; }
    }
}
