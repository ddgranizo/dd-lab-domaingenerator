using DD.Lab.Wpf.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Commands
{
    public class RelayCommandHandled : RelayCommand
    {

        public RelayCommandHandled() : this(null, null)
        {
        }

        public RelayCommandHandled(Action<object> execute) : this(execute, null)
        {

        }

        public RelayCommandHandled(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {

        }

        public override void Execute(object parameter)
        {
            try
            {
                base.Execute(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command.{Environment.NewLine}Error description: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//#if DEBUG
//                throw;
//#endif
            }
            
        }
    }
}
