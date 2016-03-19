using mosaic.ui.EventAggregation;

namespace mosaic.ui.ResolutionSettings
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