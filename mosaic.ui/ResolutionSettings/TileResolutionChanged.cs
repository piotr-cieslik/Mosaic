using mosaic.ui.EventAggregation;

namespace mosaic.ui.ResolutionSettings
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