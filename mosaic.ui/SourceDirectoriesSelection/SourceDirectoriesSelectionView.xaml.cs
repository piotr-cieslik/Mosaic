namespace mosaic.ui.SourceDirectoriesSelection
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