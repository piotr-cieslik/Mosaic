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
            Status = ":(";
            Maximum = 50;
            Value = 10;
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
    }
}