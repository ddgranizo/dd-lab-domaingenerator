using DD.Lab.Wpf.Events;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf
{

    public delegate void WpfEntityReferenceInputClickEventHandler(object sender, WpfEntityReferenceClickEventArgs args);
    public delegate void WpfCustomModuleClickEventHandler(object sender, WpfCustomModuleEventArgs args);

    public class WpfEventManager
    {
        public event WpfEntityReferenceInputClickEventHandler OnEntityReferenceInputLeftClicked;
        public event WpfCustomModuleClickEventHandler OnCustomModuleEditingRequest;


        public WpfEventManager()
        {
        }

        public void RaiseOnInputClicked(object sender, string entityLogicalName, Guid id)
        {
            OnEntityReferenceInputLeftClicked?.Invoke(sender, new WpfEntityReferenceClickEventArgs(entityLogicalName, id));
        }

        public string RaiseOnCustomModuleClicked(object sender, string moduleName, string content)
        {
            var args = new WpfCustomModuleEventArgs(moduleName, ref content);
            OnCustomModuleEditingRequest?.Invoke(sender, args);
            return args.Content;
        }
    }
}
