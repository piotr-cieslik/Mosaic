using mosaic.ui.EventAggregation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace mosaic.ui.SourceDirectoriesSelection
{
    internal sealed class SourceDirectoriesSelectionViewModel
    {
        private EventAggregator _eventAggregator;

        public SourceDirectoriesSelectionViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();

            SourceDirectoryPaths = new ObservableCollection<string>();

            AddSourceDirectoryCommand = new AddSourceDirectory(_eventAggregator);
            RemoveSourceDirectoryCommand = new RemoveSourceDirectory(_eventAggregator);

            _eventAggregator.Subscribe<SourceDirectoryAdded>(OnSourceDirectoryAdded);
            _eventAggregator.Subscribe<SourceDirectoryRemoved>(OnSourceDirectoryRemoved);
        }

        public ICommand AddSourceDirectoryCommand { get; }

        public ICommand RemoveSourceDirectoryCommand { get; }

        public string SelectedSourceDirectoryPath
        {
            set { _eventAggregator.Publish(new SourceDirectorySelected(value)); }
        }

        public ObservableCollection<string> SourceDirectoryPaths { get; }

        private void OnSourceDirectoryAdded(SourceDirectoryAdded message)
        {
            SourceDirectoryPaths.Add(message.Path);
        }

        private void OnSourceDirectoryRemoved(SourceDirectoryRemoved message)
        {
            SourceDirectoryPaths.Remove(message.Path);
        }
    }
}