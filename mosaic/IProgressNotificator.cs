namespace mosaic
{
    public interface IProgressNotificator
    {
        void NotifyGeneratingProgress(int value, int maximum);

        void NotifyPreprocesingProgress(int value, int maximum);
    }
}