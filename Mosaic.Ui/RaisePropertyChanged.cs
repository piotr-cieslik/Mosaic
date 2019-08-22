using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mosaic.Ui
{
    internal static class RaisePropertyChanged
    {
        public static void Raise(this PropertyChangedEventHandler handler, object sender, [CallerMemberName] string propertyName = "")
        {
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}