namespace mosaic.ui.BaseImageSelection
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