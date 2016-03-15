using mosaic.Directories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace mosaic.ui
{
    internal sealed class MosaicViewModel : INotifyPropertyChanged
    {
        private string _baseImagePath;
        private string _outputDirectory;

        public MosaicViewModel()
        {
            ChooseBaseImageCommand = new DelegateCommand(ChooseBaseImage);
            ChooseOutputDirectoryCommand = new DelegateCommand(ChooseOutputDirectory);
            AddSourceDirectoryCommand = new DelegateCommand(AddSourceDirectory);
            RemoveSourceDirectoryCommand = new DelegateCommand(RemoveSourceDirectory);
            GenerateCommand = new DelegateCommand(Generate);
            SourceDirectoryPaths = new ObservableCollection<string>();
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

        public string OutputDirectory
        {
            get { return _outputDirectory; }
            set
            {
                _outputDirectory = value;
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

        private void ChooseBaseImage()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    BaseImagePath = dialog.FileName;
                }
            }
        }

        private void ChooseOutputDirectory()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    OutputDirectory = dialog.SelectedPath;
                }
            }
        }

        private void Generate()
        {
            var sourceDirectory = new SourceDirectory(SourceDirectoryPaths);
            var outputDirectory = new OutputDirectory(_outputDirectory);
            var generator = new MosaicGenerator(sourceDirectory, outputDirectory);
            generator.Generate(_baseImagePath, 320, 240, 25);
        }

        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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