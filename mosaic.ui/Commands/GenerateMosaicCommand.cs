using mosaic.Directories;
using mosaic.ui.EventAggregation;
using mosaic.ui.Messages;
using mosaic.ui.ProgressNotification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mosaic.ui.Commands
{
    internal sealed class GenerateMosaicCommand : ICommand
    {
        private readonly EventAggregator _eventAggregator;
        private string _baseImagePath;
        private string _outputDirectoryPath;
        private List<string> _sourceDirectoryPaths = new List<string>();

        public GenerateMosaicCommand(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<BaseImageChanged>(OnBaseImageChanged);
            _eventAggregator.Subscribe<OutputDirectoryChanged>(OnOutputDirectoryChanged);
            _eventAggregator.Subscribe<SourceDirectoryAdded>(OnSourceDirectoryAdded);
            _eventAggregator.Subscribe<SourceDirectoryRemoved>(OnSourceDirectoryRemoved);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_outputDirectoryPath) && !string.IsNullOrEmpty(_baseImagePath) && _sourceDirectoryPaths.Count != 0;
        }

        public async void Execute(object parameter)
        {
            var sourceDirectory = new SourceDirectory(_sourceDirectoryPaths);
            var outputDirectory = new OutputDirectory(_outputDirectoryPath);
            var progressNotificator = new ProgressNotificator(_eventAggregator);
            var generator = new MosaicGenerator(sourceDirectory, outputDirectory, progressNotificator);
            await Task.Run(() => generator.Generate(_baseImagePath, 160, 120, 50));
        }

        private void OnBaseImageChanged(BaseImageChanged message)
        {
            _baseImagePath = message.Path;
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
    }
}