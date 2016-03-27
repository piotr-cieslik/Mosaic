using mosaic.ColorSpaces;
using mosaic.Directories;
using mosaic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mosaic
{
    internal sealed class TilesCollection
    {
        private const float BaseTolerance = 0.1f;
        private readonly IProgressNotificator _progressNotificator;
        private readonly Random _random;
        private readonly ISourceDirectory _sourceDirectory;
        private readonly List<Tile> _tiles;

        public TilesCollection(ISourceDirectory sourceDirectory, IProgressNotificator progressNotificator)
        {
            _sourceDirectory = sourceDirectory;
            _progressNotificator = progressNotificator;
            _tiles = new List<Tile>();
            _random = new Random(0);
        }

        public void Fill(int tileSize)
        {
            var paths = _sourceDirectory.GetPaths();
            var totalImages = paths.Count;
            var processedImages = 0;

            if (paths.Count == 0)
            {
                throw new NoImagesFoundException();
            }

            foreach (var path in paths)
            {
                using (var image = _sourceDirectory.GetImage(path))
                {
                    var resizedImage = SquareImageGenerator.Resize(image, tileSize);
                    var averageHsvValue = AverageHsvValueCalculator.Calculate(resizedImage);
                    _tiles.Add(new Tile(resizedImage, averageHsvValue));
                }
                processedImages++;
                _progressNotificator.NotifyPreprocesingProgress(processedImages, totalImages);
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