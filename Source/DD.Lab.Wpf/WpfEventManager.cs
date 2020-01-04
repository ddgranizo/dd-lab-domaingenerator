using DD.Lab.Wpf.Events;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf
{

    public delegate void WpfEntityReferenceInputClickEventHandler(object sender, WpfEntityReferenceClickEventArgs args);
    public class WpfEventManager
    {
        public event WpfEntityReferenceInputClickEventHandler OnEntityReferenceInputLeftClicked;

        public WpfEventManager()
        {
        }

        public void RaiseOnInputClicked(object sender, string entityLogicalName, Guid id)
        {
            OnEntityReferenceInputLeftClicked?.Invoke(sender, new WpfEntityReferenceClickEventArgs(entityLogicalName, id));
        }
    }
}
