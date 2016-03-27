using mosaic.ui.UserNotification;
using System.Windows;

namespace mosaic.ui
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