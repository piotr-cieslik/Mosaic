using mosaic.ui.EventAggregation;
using mosaic.ui.MosaicGeneration;
using System;
using System.Windows;

namespace mosaic.ui.UserNotification
{
    internal sealed class UserNotificator
    {
        private EventAggregator _eventAggregator;

        public UserNotificator()
        {
            _eventAggregator = EventAggregatorProvider.GetInstance();

            _eventAggregator.Subscribe<MosaicGeneratedSuccessfully>(OnMosaicGeneratedSuccessfully);
            _eventAggregator.Subscribe<NoImagesFound>(OnNoImagesFound);
        }

        private void OnMosaicGeneratedSuccessfully(MosaicGeneratedSuccessfully message)
        {
            MessageBox.Show(
                "Gotowe!",
                string.Empty,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void OnNoImagesFound(NoImagesFound message)
        {
            var error = "Wybrane katalogi źródłowe nie zawierają obrazów." + Environment.NewLine + "Wybierz inne i spróbuj ponownie.";
            MessageBox.Show(error, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}