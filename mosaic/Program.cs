using mosaic.Directories;
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

            var sourceDirectory = new SourceDirectory(SourceDirectory);
            var temporaryDirectory = new TemporaryDirectory(TemporaryDirectory);
            var outputDirectory = new OutputDirectory(OutputDirectory);

            var sourceImagePreprocesor = new SourceImagesPreprocesor(
                sourceDirectory,
                temporaryDirectory);
            sourceImagePreprocesor.Run();

            var mosaicGenerator = new MosaicGenerator(
                sourceDirectory,
                temporaryDirectory,
                outputDirectory);
            mosaicGenerator.Generate(SourceImageFileName, 160, 120, 100);
        }
    }
}