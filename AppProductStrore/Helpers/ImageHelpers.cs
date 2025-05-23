using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace AppProductStrore.Helpers
{
    public static class ImageHelpers
    {
        public static void SetDefaultImageOnFail(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image img)
            {
                img.Source = new BitmapImage(new Uri("ms-appdata:///local/Images/productDef.png"));
            }
        }
    }
}
