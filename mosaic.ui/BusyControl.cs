using System.Windows;
using System.Windows.Controls;

namespace mosaic.ui
{
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_BusyContent", Type = typeof(ContentPresenter))]
    internal sealed class BusyControl : ContentControl
    {
        public static readonly DependencyProperty BusyContentProperty =
            DependencyProperty.Register("BusyContent", typeof(object), typeof(BusyControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsBusyProperty =
                    DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, OnIsBusyChanged));

        private ContentPresenter _busyContent;

        private ContentPresenter _content;

        static BusyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyControl), new FrameworkPropertyMetadata(typeof(BusyControl)));
        }

        public object BusyContent
        {
            get { return (object)GetValue(BusyContentProperty); }
            set { SetValue(BusyContentProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Template == null)
            {
                return;
            }

            _busyContent = Template.FindName("PART_BusyContent", this) as ContentPresenter;
            if (_busyContent != null)
            {
                //BindingOperations.SetBinding(_busyContent, ContentControl.ContentProperty, new Binding("BusyContent") { Source = this });
                _busyContent.ContentSource = "BusyContent";
            }

            _content = Template.FindName("PART_Content", this) as ContentPresenter;
            if (_content != null)
            {
                _content.ContentSource = "Content";
            }

            Update();
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var busyControl = (BusyControl)d;
            busyControl.Update();
        }

        private void Update()
        {
            if (_busyContent != null)
            {
                _busyContent.Visibility = IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
            if (_content != null)
            {
                _content.IsEnabled = !IsBusy;
            }
        }
    }
}