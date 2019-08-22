using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.ResolutionSettings
{
    internal class ImageResolutionChanged : IMessage
    {
        public ImageResolutionChanged(ImageResolution imageResolution)
        {
            ImageResolution = imageResolution;
        }

        public ImageResolution ImageResolution { get; }
    }
}