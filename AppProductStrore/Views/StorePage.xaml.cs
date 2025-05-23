using AppProductStrore.Models;
using AppProductStrore.Services;
using AppProductStrore.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppProductStrore.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
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
            if (sender is Image img)
            {
                img.Source = new BitmapImage(new Uri("ms-appdata:///local/Images/productDef.png"));
            }
        }
    }
}
