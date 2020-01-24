using DD.Lab.Wpf.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf.Commands.Base
{
    public class RelayCommand : ICommand
    {
        private  Action<object> _execute;
        private  Predicate<object> _canExecute;

        public RelayCommand()
            : this(null, null)
        {
        }

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {

        }


        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            Initialize(execute, canExecute);
        }

        public void Initialize(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
            if (canExecute == null)
            {
                _canExecute = (data) => { return true; };
            }
        }

        public void Initialize(Action<object> execute)
        {
            Initialize(execute, null);
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;


        public virtual void Execute(object parameter)
        {
            IntPtr cursor = IntPtr.Zero;
            try
            {
                cursor = System32.GetCursor();
                IntPtr handle = System32.LoadCursor(new HandleRef(), 0x7f02);
                HandleRef inst = new HandleRef(parameter, handle);
                System32.SetCursor(inst);
                if (_execute == null)
                {
                    throw new Exception("Execute action is null");
                }
                _execute(parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                System32.SetCursor(new HandleRef(parameter, cursor));
            }
        }

        public void RaiseCanExecuteChanged(object param = null)
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(param, new EventArgs());
            }
        }
    }
}
