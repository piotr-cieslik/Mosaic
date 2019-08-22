using Mosaic.Ui.BaseImageSelection;
using Mosaic.Ui.EventAggregation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Mosaic.Ui.ResolutionSettings
{
    internal sealed class ResolutionSettingsViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;

        private int _customNumberOfTilesHorizaontally;
        private int _customNumberOfTilesVertically;
        private bool _customResolution;
        private int _customTileResolution;
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
            _eventAggregator.Subscribe<ImageResolutionChanged>(OnImageResolutionChanged);
            _eventAggregator.Subscribe<TileResolutionChanged>(OnTileResolutionChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int CustomNumberOfTilesHorizaontally
        {
            get
            {
                return _customNumberOfTilesHorizaontally;
            }

            set
            {
                if (_customNumberOfTilesHorizaontally == value)
                {
                    return;
                }
                _customNumberOfTilesHorizaontally = value;
                _eventAggregator.Publish(new ImageResolutionChanged(new ImageResolution(_customNumberOfTilesHorizaontally, _customNumberOfTilesVertically)));
                PropertyChanged.Raise(this);
            }
        }

        public int CustomNumberOfTilesVertically
        {
            get
            {
                return _customNumberOfTilesVertically;
            }

            set
            {
                if (_customNumberOfTilesVertically == value)
                {
                    return;
                }
                _customNumberOfTilesVertically = value;

                _eventAggregator.Publish(new ImageResolutionChanged(new ImageResolution(_customNumberOfTilesHorizaontally, _customNumberOfTilesVertically)));
                PropertyChanged.Raise(this);
            }
        }

        public bool CustomResolution
        {
            get
            {
                return _customResolution;
            }

            set
            {
                if (_customResolution == value)
                {
                    return;
                }
                _customResolution = value;

                SelectedTileResolution = SelectedTileResolution;
                SelectedImageResolution = SelectedImageResolution;
                PropertyChanged.Raise(this);
            }
        }

        public int CustomTileResolution
        {
            get
            {
                return _customTileResolution;
            }

            set
            {
                if (_customTileResolution == value)
                {
                    return;
                }
                _customTileResolution = value;
                _eventAggregator.Publish(new TileResolutionChanged(new TileResolution(value)));
                PropertyChanged.Raise(this);
            }
        }

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
            var horizontalResolution = 0;
            var verticalResolution = 0;

            if (CustomResolution)
            {
                horizontalResolution = _customTileResolution * _customNumberOfTilesHorizaontally;
                verticalResolution = _customTileResolution * _customNumberOfTilesVertically;
            }
            else
            {
                horizontalResolution = SelectedTileResolution.Resolution * SelectedImageResolution.NumberOfTilesHorizaontally;
                verticalResolution = SelectedTileResolution.Resolution * SelectedImageResolution.NumberOfTilesVertically;
            }

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

        private void OnImageResolutionChanged(ImageResolutionChanged message)
        {
            CustomNumberOfTilesHorizaontally = message.ImageResolution.NumberOfTilesHorizaontally;
            CustomNumberOfTilesVertically = message.ImageResolution.NumberOfTilesVertically;
        }

        private void OnTileResolutionChanged(TileResolutionChanged message)
        {
            CustomTileResolution = message.TileResolution.Resolution;
        }
    }
}