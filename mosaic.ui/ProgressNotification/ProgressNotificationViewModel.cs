using mosaic.ui.EventAggregation;
using mosaic.ui.MosaicGeneration;
using System.ComponentModel;

namespace mosaic.ui.ProgressNotification
{
    internal sealed class ProgressNotificationViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;
        private int _imageProcessingProgressMaximum;
        private int _imageProcessingProgressValue;
        private bool _isActive;
        private int _mosaiGenerationProgressMaximum;
        private int _mosaiGenerationProgressValue;

        public ProgressNotificationViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            ResetProgressValues();

            _eventAggregator.Subscribe<ProcessedImage>(OnProcessedImage);
            _eventAggregator.Subscribe<GeneratedTile>(OnGeneratedTile);
            _eventAggregator.Subscribe<MosaicGeneratedSuccessfully>(OnMosaicGeneratedSuccessfully);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int ImageProcessingProgressMaximum
        {
            get
            {
                return _imageProcessingProgressMaximum;
            }

            set
            {
                _imageProcessingProgressMaximum = value;
                PropertyChanged.Raise(this);
            }
        }

        public int ImageProcessingProgressValue
        {
            get
            {
                return _imageProcessingProgressValue;
            }

            set
            {
                _imageProcessingProgressValue = value;
                PropertyChanged.Raise(this);
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                PropertyChanged.Raise(this);
            }
        }

        public int MosaiGenerationProgressMaximum
        {
            get
            {
                return _mosaiGenerationProgressMaximum;
            }

            set
            {
                _mosaiGenerationProgressMaximum = value;
                PropertyChanged.Raise(this);
            }
        }

        public int MosaiGenerationProgressValue
        {
            get
            {
                return _mosaiGenerationProgressValue;
            }

            set
            {
                _mosaiGenerationProgressValue = value;
                PropertyChanged.Raise(this);
            }
        }

        private void OnGeneratedTile(GeneratedTile message)
        {
            MosaiGenerationProgressMaximum = message.Maximum;
            MosaiGenerationProgressValue = message.Value;
        }

        private void OnMosaicGeneratedSuccessfully(MosaicGeneratedSuccessfully obj)
        {
            IsActive = false;
            ResetProgressValues();
        }

        private void OnProcessedImage(ProcessedImage message)
        {
            IsActive = true;
            ImageProcessingProgressValue = message.Value;
            ImageProcessingProgressMaximum = message.Maximum;
        }

        private void ResetProgressValues()
        {
            ImageProcessingProgressMaximum = 1;
            MosaiGenerationProgressMaximum = 1;
            ImageProcessingProgressValue = 0;
            MosaiGenerationProgressValue = 0;
        }
    }
}