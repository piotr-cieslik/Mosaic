using System;
using System.Windows.Input;

namespace mosaic.ui
{
    internal sealed class DelegateCommand : ICommand
    {
        private Action _execute;

        public DelegateCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}