using mosaic.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mosaic
{
    internal sealed class SimilarTemporaryImageFinder
    {
        private const float BaseTolerance = 0.1f;
        private readonly Random _random;
        private readonly IReadOnlyCollection<TemporaryImage> _temporaryImages;

        public SimilarTemporaryImageFinder(IReadOnlyCollection<TemporaryImage> temporaryImages)
        {
            _temporaryImages = temporaryImages;
            _random = new Random(0);
        }

        public TemporaryImage Find(Hsv hsv)
        {
            return Find(hsv, BaseTolerance);
        }

        private TemporaryImage Find(Hsv hsv, float tolerance)
        {
            var similarImages = _temporaryImages.Where(ti => ti.AverageHsvValue > hsv.V - tolerance && ti.AverageHsvValue < hsv.V + tolerance).ToArray();

            if (similarImages.Length == 0)
            {
                return Find(hsv, tolerance * 2);
            }

            var index = _random.Next(similarImages.Length);
            return similarImages[index];
        }
    }
}