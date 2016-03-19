using mosaic.ui.EventAggregation;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace mosaic.ui.BaseImageSelection
{
    internal sealed class SelectBaseImage : ICommand
    {
        private readonly EventAggregator _eventAggregator;

        public SelectBaseImage(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _eventAggregator.Publish(new BaseImageSelected(dialog.FileName));
                }
            }
        }
    }
}