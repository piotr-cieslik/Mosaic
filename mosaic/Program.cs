using System.IO;

namespace mosaic
{
    internal class Program
    {
        public const string OutputDirectory = SourceDirectory + @"result\";
        public const string SourceDirectory = @"C:\Users\Piotr\Desktop\mosaic\";
        public const string SourceImageFileName = "01.jpg";
        public const string TemporaryDirectory = SourceDirectory + @"temp\";

        private static void ClearTemporaryStorage()
        {
            if (Directory.Exists(TemporaryDirectory))
            {
                Directory.Delete(TemporaryDirectory, true);
            }
            Directory.CreateDirectory(TemporaryDirectory);
        }

        private static void Main(string[] args)
        {
            ClearTemporaryStorage();
            Directory.CreateDirectory(OutputDirectory);

            var imageProvider = new ImageProvider(SourceDirectory);
            var temporaryDirectory = new TemporaryDirectory(TemporaryDirectory);
            var mosaicPersister = new MosaicPersister(OutputDirectory);

            var sourceImagePreprocesor = new SourceImagesPreprocesor(
                imageProvider,
                temporaryDirectory);
            sourceImagePreprocesor.Run();

            var mosaicGenerator = new MosaicGenerator(
                imageProvider,
                temporaryDirectory,
                mosaicPersister);
            mosaicGenerator.Generate(SourceImageFileName, 160, 120, 100);
        }
    }
}