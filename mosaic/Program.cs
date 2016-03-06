using System;

namespace mosaic
{
    internal class Program
    {
        public static string ImageInformationsFileName = "image_information.txt";
        public static string SourceDirectory = @"C:\Users\Piotr\Desktop\mosaic";
        public static string TemporaryDirectory = SourceDirectory + @"temp\";

        private static void Main(string[] args)
        {
            var sourceImagesProvider = new SourceImagesProvider(SourceDirectory);
            var temporaryImageStorage = new TemporaryImageStorage();
            var temporaryImageInformationStorage = new TemporaryImageInformationStorage(TemporaryDirectory, ImageInformationsFileName);

            var sourceImagePreprocesor = new SourceImagesPreprocesor(
                sourceImagesProvider,
                temporaryImageStorage,
                temporaryImageInformationStorage);

            sourceImagePreprocesor.Run();
            Console.ReadKey();
        }
    }
}