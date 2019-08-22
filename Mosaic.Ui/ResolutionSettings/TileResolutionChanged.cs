using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.ResolutionSettings
{
    internal sealed class TileResolutionChanged : IMessage
    {
        public TileResolutionChanged(TileResolution tileResolution)
        {
            TileResolution = tileResolution;
        }

        public TileResolution TileResolution { get; }
    }
}