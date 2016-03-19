using mosaic.ui.EventAggregation;
using System.ComponentModel;
using System.Windows.Input;

namespace mosaic.ui.BaseImageSelection
{
    internal sealed class BaseImageSelectionViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;
        private string _baseImagePath;

        public BaseImageSelectionViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            SelectBaseImageCommand = new SelectBaseImage(_eventAggregator);

            _eventAggregator.Subscribe<BaseImageSelected>(OnBaseImageChanged);
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

        public ICommand SelectBaseImageCommand { get; }

        private void OnBaseImageChanged(BaseImageSelected message)
        {
            BaseImagePath = message.Path;
        }
    }
}