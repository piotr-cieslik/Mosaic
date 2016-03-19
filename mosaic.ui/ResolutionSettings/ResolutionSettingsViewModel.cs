using mosaic.ui.EventAggregation;
using System.Collections.Generic;
using System.ComponentModel;

namespace mosaic.ui.ResolutionSettings
{
    internal sealed class ResolutionSettingsViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;

        private ImageResolution _selectedImageResolution;
        private TileResolution _selectedTileResolution;

        public ResolutionSettingsViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();

            TileResolutions = new[]
            {
                new TileResolution(25),
                new TileResolution(50),
                new TileResolution(75),
                new TileResolution(100)
            };

            ImageResolutions = new[]
            {
                new ImageResolution(160, 120),
                new ImageResolution(320, 240),
                new ImageResolution(120, 160),
                new ImageResolution(240, 320)
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ImageResolution> ImageResolutions { get; }

        public ImageResolution SelectedImageResolution
        {
            get
            {
                return _selectedImageResolution;
            }

            set
            {
                _selectedImageResolution = value;
                _eventAggregator.Publish(new ImageResolutionChanged(value));
                PropertyChanged.Raise(this);
            }
        }

        public TileResolution SelectedTileResolution
        {
            get
            {
                return _selectedTileResolution;
            }

            set
            {
                _selectedTileResolution = value;
                _eventAggregator.Publish(new TileResolutionChanged(value));
                PropertyChanged.Raise(this);
            }
        }

        public IEnumerable<TileResolution> TileResolutions { get; }
    }
}