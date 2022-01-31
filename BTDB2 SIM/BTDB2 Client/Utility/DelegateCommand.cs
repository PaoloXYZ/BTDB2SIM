using System;
using System.Windows.Input;

namespace BTDB2_Client.Utility
{
    class DelegateCommand<T> : ICommand
    {
        private readonly Action<object> ExecuteAction;
        private readonly Func<object, bool> CanExecuteFunc;
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            ExecuteAction = executeAction;
            CanExecuteFunc = canExecuteFunc;
        }
        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc(parameter);
        }
        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
