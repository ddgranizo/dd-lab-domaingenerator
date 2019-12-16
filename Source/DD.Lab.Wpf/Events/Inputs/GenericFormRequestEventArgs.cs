using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class GenericFormRequestEventArgs : EventArgs
    {
        public GenericFormRequestEventArgs(Guid requestId, GenericFormModel formModel, Dictionary<string, object> initialValues)
        {
            RequestId = requestId;
            FormModel = formModel;
            InitialValues = initialValues;
        }

        public GenericFormRequestEventArgs(Guid requestId, GenericFormModel formModel)
            :this(requestId, formModel, new Dictionary<string, object>())
        {
           
        }

        public Guid RequestId { get; }
        public GenericFormModel FormModel { get; }
        public Dictionary<string, object> InitialValues { get; }
    }
}
