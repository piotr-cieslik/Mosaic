using mosaic.ui.EventAggregation;
using mosaic.ui.Messages;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace mosaic.ui.Commands
{
    internal sealed class AddSourceDirectoryCommand : ICommand
    {
        private readonly EventAggregator _eventAggregator;

        public AddSourceDirectoryCommand(EventAggregator eventAggregator)
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
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _eventAggregator.Publish(new SourceDirectoryAdded(dialog.SelectedPath));
                }
            }
        }
    }
}