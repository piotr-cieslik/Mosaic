namespace mosaic.ui.ProgressNotification
{
    public partial class ProgressNotificationView
    {
        public ProgressNotificationView()
        {
            InitializeComponent();
            DataContext = new ProgressNotificationViewModel();
        }
    }
}