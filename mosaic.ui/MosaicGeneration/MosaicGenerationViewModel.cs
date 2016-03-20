using mosaic.ui.EventAggregation;
using System.Windows.Input;

namespace mosaic.ui.MosaicGeneration
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