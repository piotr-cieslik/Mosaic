namespace mosaic.ui.ResolutionSettings
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