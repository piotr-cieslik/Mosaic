namespace mosaic.ui.MosaicGeneration
{
    public partial class MosaicGenerationView
    {
        public MosaicGenerationView()
        {
            InitializeComponent();
            DataContext = new MosaicGenerationViewModel();
        }
    }
}