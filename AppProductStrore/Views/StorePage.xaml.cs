using AppProductStrore.Helpers;
using AppProductStrore.Services;
using AppProductStrore.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace AppProductStrore.Views
{
    public sealed partial class StorePage : Page
    {
        public StoreViewModel ViewModel { get; }
        public StorePage()
        {
            this.InitializeComponent();
            ViewModel = new StoreViewModel(new ProductService());
            DataContext = ViewModel;
        }

        private void ProductImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ImageHelpers.SetDefaultImageOnFail(sender, e);
        }
    }
}
