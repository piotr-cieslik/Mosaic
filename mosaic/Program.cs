using System;

namespace mosaic
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var imageSizeNormalizer = new ImageSizeNormalizer();
            imageSizeNormalizer.Normalize(Constants.SourceFilesPath, Constants.TemporaryFilesPath, 100);

            Console.ReadKey();
        }
    }
}