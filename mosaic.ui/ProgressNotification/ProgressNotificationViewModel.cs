using mosaic.ui.EventAggregation;
using System.ComponentModel;

namespace mosaic.ui.ProgressNotification
{
    internal sealed class ProgressNotificationViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;
        private int _maximum;

        private string _status;

        private int _value;

        public ProgressNotificationViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            Maximum = 1;
            Value = 0;

            _eventAggregator.Subscribe<ProcessedImage>(OnProcessedImage);
            _eventAggregator.Subscribe<GeneratedTile>(OnGeneratedTile);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                PropertyChanged.Raise(this);
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                PropertyChanged.Raise(this);
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                PropertyChanged.Raise(this);
            }
        }

        private void OnGeneratedTile(GeneratedTile message)
        {
            Status = string.Format("Generowanie {0} / {1}", message.Value, message.Maximum);
            Value = message.Value;
            Maximum = message.Maximum;
        }

        private void OnProcessedImage(ProcessedImage message)
        {
            Status = string.Format("Przetwarzanie {0} / {1}", message.Value, message.Maximum);
            Value = message.Value;
            Maximum = message.Maximum;
        }
    }
}