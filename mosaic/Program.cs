using System;

namespace mosaic
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var progressMonitor = new ProgressMonitor();
            var imageSizeNormalizer = new ImageSizeNormalizer(progressMonitor);
            imageSizeNormalizer.Normalize(Constants.SourceFilesPath, Constants.TemporaryFilesPath, 100);

            Console.ReadKey();
        }
    }
}