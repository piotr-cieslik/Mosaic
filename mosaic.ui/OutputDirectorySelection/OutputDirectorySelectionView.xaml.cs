namespace mosaic.ui.OutputDirectorySelection
{
    public partial class OutputDirectorySelectionView
    {
        public OutputDirectorySelectionView()
        {
            InitializeComponent();
            DataContext = new OutputDirectorySelectionViewModel();
        }
    }
}