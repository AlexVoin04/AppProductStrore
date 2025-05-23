using AppProductStrore.Helpers;
using AppProductStrore.Interfaces;
using AppProductStrore.ViewModels;
using AppProductStrore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppProductStrore.Models
{
    public class MainViewModel: BaseViewModel
    {
        private readonly INavigationService _navigationService;

        private string _selectedTag;
        public string SelectedTag
        {
            get => _selectedTag;
            set
            {
                if (_selectedTag != value)
                {
                    _selectedTag = value;
                    OnPropertyChanged(nameof(SelectedTag));
                    ExecuteNavigation(_selectedTag);
                }
            }
        }

        public ICommand NavigateCommand { get; }
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateCommand = new RelayCommand(ExecuteNavigation);

            ExecuteNavigation("store");
        }

        public void ExecuteNavigation(object parameter)
        {
            if (parameter is string tag)
            {
                switch (tag)
                {
                    case "store":
                        _navigationService.NavigateTo(typeof(StorePage));
                        break;
                    case "cart":
                        _navigationService.NavigateTo(typeof(CartPage));
                        break;
                }
            }
        }
    }
}
