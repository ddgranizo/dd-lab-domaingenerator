using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models
{
    public class PropertiesTrigger
    {
        public enum TriggerMode
        {
            OnlyOnce = 1,
            EveryChange = 2,
        }

        public PropertiesTrigger(Action action, params string[] properties)
        {
            Properties = properties;
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Mode = TriggerMode.EveryChange;
            SetNullParametersWhenExecutedOnce = true;
        }

        public string[] Properties { get; set; }
        public Action Action { get; set; }
        public TriggerMode Mode { get; set; }
        public int ExecutionTimes { get; set; }
        public bool SetNullParametersWhenExecutedOnce { get; set; }
    }
}
