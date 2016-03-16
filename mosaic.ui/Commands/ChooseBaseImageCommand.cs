using mosaic.ui.EventAggregation;
using mosaic.ui.Messages;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace mosaic.ui.Commands
{
    internal sealed class ChooseBaseImageCommand : ICommand
    {
        private readonly EventAggregator _eventAggregator;

        public ChooseBaseImageCommand(EventAggregator eventAggregator)
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
                    _eventAggregator.Publish(new BaseImageChanged(dialog.FileName));
                }
            }
        }
    }
}