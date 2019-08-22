namespace Mosaic.Ui.OutputDirectorySelection
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