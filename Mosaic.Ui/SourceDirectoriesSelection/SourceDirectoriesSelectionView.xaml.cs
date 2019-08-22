namespace Mosaic.Ui.SourceDirectoriesSelection
{
    public partial class SourceDirectoriesSelectionView
    {
        public SourceDirectoriesSelectionView()
        {
            InitializeComponent();
            DataContext = new SourceDirectoriesSelectionViewModel();
        }
    }
}