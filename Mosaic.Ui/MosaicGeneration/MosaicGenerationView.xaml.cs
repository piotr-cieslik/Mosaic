namespace Mosaic.Ui.MosaicGeneration
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