using System.Windows;

namespace mosaic.ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MosaicView : Window
    {
        public MosaicView()
        {
            InitializeComponent();
            DataContext = new MosaicViewModel();
        }
    }
}