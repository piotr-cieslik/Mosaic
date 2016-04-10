using System;
using System.Windows;
using mosaic.ui.EventAggregation;
using mosaic.ui.MosaicGeneration;

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
            _eventAggregator.Subscribe<OutputImageIsToLarge>(OnOutputImageIsToLarge);
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

        private void OnOutputImageIsToLarge(OutputImageIsToLarge message)
        {
            var error = "Obraz wyjściowy jest zbyt duży." + Environment.NewLine +
                "Rozwiązania:" + Environment.NewLine +
                "- zmniejsz liczbę kafelek w obrazie wyjściowym," + Environment.NewLine +
                "- pomniejsz rozmiar pojedynczej kafelki.";
            MessageBox.Show(error, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}