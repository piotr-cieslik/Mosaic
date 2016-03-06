namespace mosaic
{
    internal class Program
    {
        public static string ImageInformationsFileName = "_image_information.txt";
        public static string SourceDirectory = @"C:\Users\Piotr\Desktop\mosaic\";
        public static string SourceImageFileName = "01.jpg";
        public static string TemporaryDirectory = SourceDirectory + @"temp\";

        private static void Main(string[] args)
        {
            var imageProvider = new ImageProvider(SourceDirectory);
            var temporaryImageStorage = new TemporaryImageStorage(TemporaryDirectory);
            var temporaryImageInformationStorage = new TemporaryImageInformationStorage(TemporaryDirectory, ImageInformationsFileName);

            temporaryImageStorage.InitializeStorage();

            var sourceImagePreprocesor = new SourceImagesPreprocesor(
                imageProvider,
                temporaryImageStorage,
                temporaryImageInformationStorage);
            sourceImagePreprocesor.Run();

            var mosaicGenerator = new MosaicGenerator(imageProvider, temporaryImageStorage, temporaryImageInformationStorage);
            mosaicGenerator.Generate(SourceImageFileName, 320, 240, 50);
        }
    }
}