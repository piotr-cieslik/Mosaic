using Mosaic.Ui.EventAggregation;
using System.Windows.Input;

namespace Mosaic.Ui.MosaicGeneration
{
    internal sealed class MosaicGenerationViewModel
    {
        private EventAggregator _eventAggregator;

        public MosaicGenerationViewModel()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();
            GenerateCommand = new Generate(_eventAggregator);
        }

        public ICommand GenerateCommand { get; }
    }
}