using mosaic.ui.Messages;
using System;
using System.Windows.Input;

namespace mosaic.ui.Commands
{
    internal sealed class RemoveSourceDirectoryCommand : ICommand
    {
        private readonly EventAggregator.EventAggregator _eventAggregator;
        private string _currentSelection;

        public RemoveSourceDirectoryCommand(EventAggregator.EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<SourceDirectorySelectionChanged>(OnSourceDirectorySelectionChanged);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _currentSelection != null;
        }

        public void Execute(object parameter)
        {
            _eventAggregator.Publish(new SourceDirectoryRemoved(_currentSelection));
        }

        private void OnSourceDirectorySelectionChanged(SourceDirectorySelectionChanged message)
        {
            _currentSelection = message.Path;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}