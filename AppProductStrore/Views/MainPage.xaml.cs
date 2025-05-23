using AppProductStrore.Interfaces;
using AppProductStrore.Models;
using AppProductStrore.Services;
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
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AppProductStrore
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly MainViewModel _viewModel;
        public MainPage()
        {
            this.InitializeComponent();
            var navigationService = new NavigationService(PageFrame);
            _viewModel = new MainViewModel(navigationService);
            this.DataContext = _viewModel;


            var view = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            view.SetPreferredMinSize(new Size(1000, 1200));

            
            MainNavView.SelectedItem = StoreNavItem;

            MainNavView.SelectionChanged += (s, e) =>
            {
                if (e.SelectedItem is NavigationViewItem navItem)
                {
                    _viewModel.SelectedTag = navItem.Tag?.ToString();
                }
            };
        }

        
    }
}
