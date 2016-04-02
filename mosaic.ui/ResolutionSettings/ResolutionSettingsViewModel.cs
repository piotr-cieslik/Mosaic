using mosaic.ui.BaseImageSelection;
using mosaic.ui.EventAggregation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace mosaic.ui.ResolutionSettings
{
    internal sealed class ResolutionSettingsViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;

        private IEnumerable<ImageResolution> _imageResolutions;
        private string _outputImageResolutionInfo;
        private ImageResolution _selectedImageResolution;
        private TileResolution _selectedTileResolution;
        private IEnumerable<TileResolution> _tileResolutions;

        public ResolutionSettingsViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            _eventAggregator.Subscribe<BaseImageSelected>(OnBaseImageSelected);
            _eventAggregator.Subscribe<ImageResolutionChanged>(x => GenerateOutputImageInfo());
            _eventAggregator.Subscribe<TileResolutionChanged>(x => GenerateOutputImageInfo());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ImageResolution> ImageResolutions
        {
            get { return _imageResolutions; }
            set
            {
                _imageResolutions = value;
                PropertyChanged.Raise(this);
            }
        }

        public string OutputImageResolutionInfo
        {
            get { return _outputImageResolutionInfo; }
            set
            {
                _outputImageResolutionInfo = value;
                PropertyChanged.Raise(this);
            }
        }

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

        public IEnumerable<TileResolution> TileResolutions
        {
            get
            {
                return _tileResolutions;
            }
            set
            {
                _tileResolutions = value;
                PropertyChanged.Raise(this);
            }
        }

        private static ImageResolution[] CreateImageResolutions(BaseImageSelected message)
        {
            // it shouldn't be performed at UI thread
            var image = Image.FromFile(message.Path);
            var ratio = image.Width / (float)image.Height;
            var numberOfTilesVertically = new[] { 50, 75, 100, 125, 150, 175, 200 };
            return numberOfTilesVertically.Select(x => new ImageResolution((int)(x * ratio), x)).ToArray();
        }

        private static TileResolution[] CreateTileResolutions()
        {
            return new[]
            {
                new TileResolution(25),
                new TileResolution(50),
                new TileResolution(75),
                new TileResolution(100)
            };
        }

        private void GenerateOutputImageInfo()
        {
            var horizontalResolution = SelectedTileResolution.Resolution * SelectedImageResolution.NumberOfTilesHorizaontally;
            var verticalResolution = SelectedTileResolution.Resolution * SelectedImageResolution.NumberOfTilesVertically;
            var resolutionInMpx = horizontalResolution * verticalResolution / 1000000;
            OutputImageResolutionInfo = string.Format("rozdzielczość obrazu wyjściowego: {0} x {1} ({2} Mpx)", horizontalResolution, verticalResolution, resolutionInMpx);
        }

        private void OnBaseImageSelected(BaseImageSelected message)
        {
            TileResolutions = CreateTileResolutions();
            ImageResolutions = CreateImageResolutions(message);

            SelectedTileResolution = TileResolutions.First();
            SelectedImageResolution = ImageResolutions.First();
        }
    }
}