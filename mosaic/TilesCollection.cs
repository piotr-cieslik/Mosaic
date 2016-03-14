using mosaic.ColorSpaces;
using mosaic.Directories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mosaic
{
    internal sealed class TilesCollection
    {
        private const float BaseTolerance = 0.1f;
        private readonly Random _random;
        private readonly ISourceDirectory _sourceDirectory;
        private readonly List<Tile> _tiles;

        public TilesCollection(ISourceDirectory sourceDirectory)
        {
            _sourceDirectory = sourceDirectory;
            _random = new Random(0);
        }

        public void Fill()
        {
            var names = _sourceDirectory.GetNames();
            foreach (var name in names)
            {
                using (var image = _sourceDirectory.GetImage(name))
                {
                    var resizedImage = SquareImageGenerator.Resize(image);
                    var averageHsvValue = AverageHsvValueCalculator.Calculate(resizedImage);
                    _tiles.Add(new Tile(resizedImage, averageHsvValue));
                }
            }
        }

        public Tile FindTileSimilarToColor(Hsv hsv)
        {
            return Find(hsv, BaseTolerance);
        }

        private Tile Find(Hsv hsv, float tolerance)
        {
            var similarImages = _tiles.Where(ti => ti.AverageHsvValue > hsv.V - tolerance && ti.AverageHsvValue < hsv.V + tolerance).ToArray();

            if (similarImages.Length == 0)
            {
                return Find(hsv, tolerance * 2);
            }

            var index = _random.Next(similarImages.Length);
            return similarImages[index];
        }
    }
}