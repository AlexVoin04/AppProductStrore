using AppProductStrore.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AppProductStrore.Views
{
    public sealed partial class CartPage : Page
    {
        public CartPage()
        {
            this.InitializeComponent();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ImageHelpers.SetDefaultImageOnFail(sender, e);
        }
    }
}
