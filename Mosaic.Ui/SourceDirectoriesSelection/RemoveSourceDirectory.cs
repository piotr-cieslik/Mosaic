using Mosaic.Ui.EventAggregation;
using System;
using System.Windows.Input;

namespace Mosaic.Ui.SourceDirectoriesSelection
{
    internal sealed class RemoveSourceDirectory : ICommand
    {
        private readonly EventAggregator _eventAggregator;
        private string _currentSelection;

        public RemoveSourceDirectory(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<SourceDirectorySelected>(OnSourceDirectorySelected);
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

        private void OnSourceDirectorySelected(SourceDirectorySelected message)
        {
            _currentSelection = message.Path;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}