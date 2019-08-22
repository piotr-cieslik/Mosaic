namespace Mosaic.Ui.ResolutionSettings
{
    public partial class ResolutionSettingsView
    {
        public ResolutionSettingsView()
        {
            InitializeComponent();
            DataContext = new ResolutionSettingsViewModel();
        }
    }
}