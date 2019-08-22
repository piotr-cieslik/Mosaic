namespace Mosaic.Ui.BaseImageSelection
{
    public partial class BaseImageSelectionView
    {
        public BaseImageSelectionView()
        {
            InitializeComponent();
            DataContext = new BaseImageSelectionViewModel();
        }
    }
}