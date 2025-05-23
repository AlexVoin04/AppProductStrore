using AppProductStrore.Models;
using AppProductStrore.Services;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;


namespace AppProductStrore
{
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
