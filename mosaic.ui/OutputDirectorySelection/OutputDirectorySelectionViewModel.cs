using mosaic.ui.EventAggregation;
using System.ComponentModel;
using System.Windows.Input;

namespace mosaic.ui.OutputDirectorySelection
{
    internal sealed class OutputDirectorySelectionViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;
        private string _outputDirectoryPath;

        public OutputDirectorySelectionViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();

            SelectOutputDirectoryCommand = new SelectOutputDirectory(_eventAggregator);
            _eventAggregator.Subscribe<OutputDirectoryChanged>(OnOutputDirectoryChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string OutputDirectoryPath
        {
            get { return _outputDirectoryPath; }
            set
            {
                _outputDirectoryPath = value;
                PropertyChanged.Raise(this);
            }
        }

        public ICommand SelectOutputDirectoryCommand { get; }

        private void OnOutputDirectoryChanged(OutputDirectoryChanged message)
        {
            OutputDirectoryPath = message.Path;
        }
    }
}