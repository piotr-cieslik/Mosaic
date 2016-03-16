using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace mosaic.ui
{
    internal static class RaisePropertyChanged
    {
        public static void Raise(this PropertyChangedEventHandler handler, object sender, [CallerMemberName] string propertyName = "")
        {
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}