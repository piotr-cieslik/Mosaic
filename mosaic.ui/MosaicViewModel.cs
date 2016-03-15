using mosaic.Directories;
using mosaic.ui.Commands;
using mosaic.ui.Messages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace mosaic.ui
{
    internal sealed class MosaicViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator.EventAggregator _eventAggregator;
        private string _baseImagePath;
        private string _outputDirectoryPath;

        public MosaicViewModel()
        {
            _eventAggregator = new EventAggregator.EventAggregator();
            ChooseBaseImageCommand = new ChooseBaseImageCommand(_eventAggregator);
            ChooseOutputDirectoryCommand = new ChooseOutputDirectoryCommand(_eventAggregator);
            AddSourceDirectoryCommand = new DelegateCommand(AddSourceDirectory);
            RemoveSourceDirectoryCommand = new DelegateCommand(RemoveSourceDirectory);
            GenerateCommand = new DelegateCommand(Generate);
            SourceDirectoryPaths = new ObservableCollection<string>();

            _eventAggregator.Subscribe<BaseImageChanged>(OnBaseImageChanged);
            _eventAggregator.Subscribe<OutputDirectoryChanged>(OnOutputDirectoryChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddSourceDirectoryCommand { get; }

        public string BaseImagePath
        {
            get { return _baseImagePath; }
            set
            {
                _baseImagePath = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ChooseBaseImageCommand { get; }

        public ICommand ChooseOutputDirectoryCommand { get; }

        public ICommand GenerateCommand { get; }

        public string OutputDirectoryPath
        {
            get { return _outputDirectoryPath; }
            set
            {
                _outputDirectoryPath = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RemoveSourceDirectoryCommand { get; }

        public string SelectedSourceDirectoryPath { get; set; }

        public ObservableCollection<string> SourceDirectoryPaths { get; }

        private void AddSourceDirectory()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SourceDirectoryPaths.Add(dialog.SelectedPath);
                }
            }
        }

        private void Generate()
        {
            var sourceDirectory = new SourceDirectory(SourceDirectoryPaths);
            var outputDirectory = new OutputDirectory(_outputDirectoryPath);
            var generator = new MosaicGenerator(sourceDirectory, outputDirectory);
            generator.Generate(_baseImagePath, 320, 240, 25);
        }

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnBaseImageChanged(BaseImageChanged message)
        {
            BaseImagePath = message.Path;
        }

        private void OnOutputDirectoryChanged(OutputDirectoryChanged message)
        {
            OutputDirectoryPath = message.Path;
        }

        private void RemoveSourceDirectory()
        {
            var selectedSourceDirectory = SelectedSourceDirectoryPath;
            if (selectedSourceDirectory != null)
            {
                SourceDirectoryPaths.Remove(selectedSourceDirectory);
            }
        }
    }
}