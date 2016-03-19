using mosaic.ui.Commands;
using mosaic.ui.EventAggregation;
using mosaic.ui.Messages;
using System.ComponentModel;
using System.Windows.Input;

namespace mosaic.ui
{
    internal sealed class MosaicViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;
        private string _baseImagePath;

        public MosaicViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            ChooseBaseImageCommand = new ChooseBaseImageCommand(_eventAggregator);
            GenerateCommand = new GenerateMosaicCommand(_eventAggregator);

            _eventAggregator.Subscribe<BaseImageChanged>(OnBaseImageChanged);
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

        public ICommand GenerateCommand { get; }

        private void OnBaseImageChanged(BaseImageChanged message)
        {
            BaseImagePath = message.Path;
        }
    }
}