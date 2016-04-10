using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using mosaic.Directories;
using mosaic.Exceptions;
using mosaic.ui.BaseImageSelection;
using mosaic.ui.EventAggregation;
using mosaic.ui.OutputDirectorySelection;
using mosaic.ui.ProgressNotification;
using mosaic.ui.ResolutionSettings;
using mosaic.ui.SourceDirectoriesSelection;

namespace mosaic.ui.MosaicGeneration
{
    internal sealed class Generate : ICommand
    {
        private readonly EventAggregator _eventAggregator;
        private string _baseImagePath;
        private ImageResolution _imageResolution;
        private string _outputDirectoryPath;
        private List<string> _sourceDirectoryPaths = new List<string>();
        private TileResolution _tileResolution;

        public Generate(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<BaseImageSelected>(OnBaseImageChanged);
            _eventAggregator.Subscribe<OutputDirectoryChanged>(OnOutputDirectoryChanged);
            _eventAggregator.Subscribe<SourceDirectoryAdded>(OnSourceDirectoryAdded);
            _eventAggregator.Subscribe<SourceDirectoryRemoved>(OnSourceDirectoryRemoved);
            _eventAggregator.Subscribe<TileResolutionChanged>(OnTileResolutionChanged);
            _eventAggregator.Subscribe<ImageResolutionChanged>(OnImageResolutionChanged);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return
                !string.IsNullOrEmpty(_outputDirectoryPath) &&
                !string.IsNullOrEmpty(_baseImagePath) &&
                _sourceDirectoryPaths.Any() &&
                !Equals(_tileResolution, default(TileResolution)) &&
                !Equals(_imageResolution, default(ImageResolution));
        }

        public async void Execute(object parameter)
        {
            var sourceDirectory = new SourceDirectory(_sourceDirectoryPaths);
            var outputDirectory = new OutputDirectory(_outputDirectoryPath);

            var progressNotificator = new ProgressNotificator(_eventAggregator);
            var generator = new MosaicGenerator(sourceDirectory, outputDirectory, progressNotificator);

            NoImagesFoundException noImageFoundException = null;
            OutputImageIsToLargeException outputImageIsToLargeException = null;
            await Task.Run(() =>
            {
                try
                {
                    generator.Generate(_baseImagePath, _imageResolution.NumberOfTilesHorizaontally, _imageResolution.NumberOfTilesVertically, _tileResolution.Resolution);
                }
                catch (NoImagesFoundException e)
                {
                    noImageFoundException = e;
                }
                catch (OutputImageIsToLargeException e)
                {
                    outputImageIsToLargeException = e;
                }
            });

            if (noImageFoundException != null)
            {
                _eventAggregator.Publish(new NoImagesFound());
                return;
            }

            if (outputImageIsToLargeException != null)
            {
                _eventAggregator.Publish(new OutputImageIsToLarge());
                return;
            }

            _eventAggregator.Publish(new MosaicGeneratedSuccessfully());
        }

        private void OnBaseImageChanged(BaseImageSelected message)
        {
            _baseImagePath = message.Path;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnImageResolutionChanged(ImageResolutionChanged message)
        {
            _imageResolution = message.ImageResolution;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnOutputDirectoryChanged(OutputDirectoryChanged message)
        {
            _outputDirectoryPath = message.Path;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnSourceDirectoryAdded(SourceDirectoryAdded message)
        {
            _sourceDirectoryPaths.Add(message.Path);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnSourceDirectoryRemoved(SourceDirectoryRemoved message)
        {
            _sourceDirectoryPaths.Remove(message.Path);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnTileResolutionChanged(TileResolutionChanged message)
        {
            _tileResolution = message.TileResolution;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}