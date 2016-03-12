using mosaic.Directories;
using System.IO;

namespace mosaic
{
    internal class Program
    {
        public const string OutputDirectory = @"C:\Users\Piotr\Desktop\mosaic\result\";
        public const string SourceDirectory = @"E:\zdjęcia\[2014-09] USA\małe\";
        public const string SourceImageFileName = "IMG_0026.jpg";
        public const string TemporaryDirectory = @"C:\Users\Piotr\Desktop\mosaic\temp\";

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
            var sourceDirectory = new SourceDirectory(SourceDirectory);
            var temporaryDirectory = new TemporaryDirectory(TemporaryDirectory);
            var outputDirectory = new OutputDirectory(OutputDirectory);

            var sourceImagePreprocesor = new SourceImagesPreprocesor(
                sourceDirectory,
                temporaryDirectory);

            //ClearTemporaryStorage();
            //Directory.CreateDirectory(OutputDirectory);
            //sourceImagePreprocesor.Run();

            var mosaicGenerator = new MosaicGenerator(
                sourceDirectory,
                temporaryDirectory,
                outputDirectory);
            mosaicGenerator.Generate(SourceImageFileName, 320, 240, 50);
        }
    }
}