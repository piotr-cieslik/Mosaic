using System;

namespace mosaic
{
    internal interface IImageSizeNormalizerProgressMonitor
    {
        void Notify(string path);
    }

    internal sealed class ProgressMonitor : IImageSizeNormalizerProgressMonitor
    {
        void IImageSizeNormalizerProgressMonitor.Notify(string path)
        {
            Console.WriteLine(path);
        }
    }
}