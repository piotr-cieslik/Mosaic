using mosaic.ui.Commands;
using mosaic.ui.EventAggregation;
using mosaic.ui.Messages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace mosaic.ui
{
    internal sealed class MosaicViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;
        private string _baseImagePath;
        private string _outputDirectoryPath;

        public MosaicViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            ChooseBaseImageCommand = new ChooseBaseImageCommand(_eventAggregator);
            ChooseOutputDirectoryCommand = new ChooseOutputDirectoryCommand(_eventAggregator);
            GenerateCommand = new GenerateMosaicCommand(_eventAggregator);

            _eventAggregator.Subscribe<BaseImageChanged>(OnBaseImageChanged);
            _eventAggregator.Subscribe<OutputDirectoryChanged>(OnOutputDirectoryChanged);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string BaseImagePath
        {
            get { return _baseImagePath; }
            set
            {
                _baseImagePath = value;
                PropertyChanged.Raise(this);
            }
        }

        public ICommand ChooseBaseImageCommand { get; }

        public ICommand ChooseOutputDirectoryCommand { get; }

        public ICommand GenerateCommand { get; }

        public string OutputDirectoryPath
        {
            get { return _outputDirectoryPath; }
            set
            {
                _outputDirectoryPath = value;
                PropertyChanged.Raise(this);
            }
        }


        private void OnBaseImageChanged(BaseImageChanged message)
        {
            BaseImagePath = message.Path;
        }

        private void OnOutputDirectoryChanged(OutputDirectoryChanged message)
        {
            OutputDirectoryPath = message.Path;
        }
    }
}