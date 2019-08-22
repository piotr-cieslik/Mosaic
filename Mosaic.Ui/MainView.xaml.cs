using Mosaic.Ui.UserNotification;
using System.Windows;

namespace Mosaic.Ui
{
    public partial class MainView : Window
    {
        private UserNotificator _userNotification;

        public MainView()
        {
            InitializeComponent();
            _userNotification = new UserNotificator();
        }
    }
}