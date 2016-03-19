using mosaic.ui.EventAggregation;
using mosaic.ui.Messages;
using System;
using System.Windows.Input;

namespace mosaic.ui.SourceDirectoriesSelection
{
    internal sealed class RemoveSourceDirectoryCommand : ICommand
    {
        private readonly EventAggregator _eventAggregator;
        private string _currentSelection;

        public RemoveSourceDirectoryCommand(EventAggregator eventAggregator)
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