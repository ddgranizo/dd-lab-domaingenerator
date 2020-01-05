using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf.Events
{
    public class WpfCustomModuleEventArgs : EventArgs
    {
        public WpfCustomModuleEventArgs(string moduleName, ref string content)
        {
            ModuleName = moduleName ?? throw new ArgumentNullException(nameof(moduleName));
            Content = content;
        }

        public string ModuleName { get; }
        public string Content { get; set;  }
    }
}
