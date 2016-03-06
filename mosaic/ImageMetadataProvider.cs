using System.Collections.Generic;

namespace mosaic
{
    internal sealed class ImageMetadataProvider
    {
        public IReadOnlyCollection<ImageMetadata> Load()
        {
            var imageMetadataCollection = new List<ImageMetadata>();
            return imageMetadataCollection;
        }
    }
}