using System;

namespace mosaic
{
    internal class Program
    {
        public static string ImageInformationsFileName = "image_information.txt";
        public static string SourceDirectory = @"C:\Users\Piotr\Desktop\mosaic\";
        public static string TemporaryDirectory = SourceDirectory + @"temp\";

        private static void Main(string[] args)
        {
            var sourceImagesProvider = new SourceImagesProvider(SourceDirectory);
            var temporaryImageStorage = new TemporaryImageStorage(TemporaryDirectory);
            var temporaryImageInformationStorage = new TemporaryImageInformationStorage(TemporaryDirectory, ImageInformationsFileName);

            temporaryImageStorage.InitializeStorage();

            var sourceImagePreprocesor = new SourceImagesPreprocesor(
                sourceImagesProvider,
                temporaryImageStorage,
                temporaryImageInformationStorage);

            sourceImagePreprocesor.Run();
            Console.ReadKey();
        }
    }
}