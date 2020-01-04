using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf.Events
{
    public class WpfEntityReferenceClickEventArgs : WpfClickEventArgs
    {
        public WpfEntityReferenceClickEventArgs(string entityLogicalName, Guid id)
        {
            EntityLogicalName = entityLogicalName ?? throw new ArgumentNullException(nameof(entityLogicalName));
            Id = id;
        }

        public string EntityLogicalName { get; }
        public Guid Id { get; }
    }
}
