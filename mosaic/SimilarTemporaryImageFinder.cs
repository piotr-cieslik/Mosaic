using mosaic.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mosaic
{
    internal sealed class SimilarTemporaryImageFinder
    {
        private readonly Random _random;
        private readonly IReadOnlyCollection<TemporaryImage> _temporaryImages;
        private const decimal Tolerance = 10m;

        public SimilarTemporaryImageFinder(IReadOnlyCollection<TemporaryImage> temporaryImages)
        {
            _temporaryImages = temporaryImages;
            _random = new Random(0);
        }

        public TemporaryImage Find(Hsv hsv)
        {
            var index = _random.Next(_temporaryImages.Count);
            return _temporaryImages.ElementAt(index);
        }
    }
}