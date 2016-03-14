using mosaic.Directories;

namespace mosaic
{
    public static class MosaicGeneratorFactory
    {
        public static MosaicGenerator Create(
            string sourceDirectoryPath,
            string outputDirectoryPath)
        {
            var sourceDirectory = new SourceDirectory(sourceDirectoryPath);
            var outputDirectory = new OutputDirectory(outputDirectoryPath);
            return new MosaicGenerator(sourceDirectory, outputDirectory);
        }
    }
}