using AppProductStrore.Helpers;
using AppProductStrore.Models;
using AppProductStrore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace AppProductStrore.ViewModels
{
    public class CartViewModel: BaseViewModel
    {
        private readonly CartService _cartService = new CartService();
        private ObservableCollection<CartItem> _cartItems = new ObservableCollection<CartItem>();
        private List<CartItem> _originalCart = new List<CartItem>();
        public ObservableCollection<CartItem> CartItems
        {
            get => _cartItems;

            set 
            { 
                _cartItems = value; OnPropertyChanged(); 
            }
        }

        public int TotalQuantity => CartItems.Sum(item => item.Quantity);
        public decimal TotalPrice => CartItems.Sum(item => item.TotalPrice);
        public string TotalQuantityText => $"Товаров: {TotalQuantity}";
        public string TotalPriceText => $"Сумма: {TotalPrice:C}";

        public ICommand RemoveFromCartCommand { get; }
        public ICommand SortCommand { get; }

        private CartSortOption _sortOption;

        public ObservableCollection<CartSortOption> SortOptions { get; } =
            new ObservableCollection<CartSortOption>
            {
                CartSortOption.Default,
                CartSortOption.ByName,
                CartSortOption.ByPrice
            };
        public CartSortOption SortOption
        {
            get => _sortOption;
            set
            {
                _sortOption = value;
                OnPropertyChanged();
                SortCartItems();
                SaveSortPreference();
            }
        }

        public CartViewModel()
        {
            RemoveFromCartCommand = new RelayCommand(async (param) => await RemoveItem(param as CartItem));
            SortCommand = new RelayCommand((param) => SortOption = (CartSortOption)param);
            LoadSortPreference();
            Task.Run(async () => await LoadCart()).Wait();
        }

        private async Task LoadCart()
        {
            var loadedItems = await _cartService.LoadCartAsync();
            _originalCart = loadedItems.ToList();
            CartItems = new ObservableCollection<CartItem>(_originalCart);
            SortCartItems();
            OnPropertyChanged(nameof(TotalQuantity));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalQuantityText));
            OnPropertyChanged(nameof(TotalPriceText));
        }

        private async Task RemoveItem(CartItem item)
        {
            CartItems.Remove(item);
            _originalCart.Remove(item); 

            await _cartService.SaveCartAsync(CartItems);
            OnPropertyChanged(nameof(TotalQuantity));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(TotalQuantityText));
            OnPropertyChanged(nameof(TotalPriceText));
        }

        private void SortCartItems()
        {
            IEnumerable<CartItem> sortedItems = _originalCart;
            switch (SortOption)
            {
                case CartSortOption.ByName:
                    sortedItems = _originalCart.OrderBy(i => i.Product.Name);
                    break;
                case CartSortOption.ByPrice:
                    sortedItems = _originalCart.OrderBy(i => i.Product.Price);
                    break;
                case CartSortOption.Default:
                default:
                    // Используем _originalCart как есть
                    break;
            }

            CartItems = new ObservableCollection<CartItem>(sortedItems);
            OnPropertyChanged(nameof(CartItems));
        }

        private void SaveSortPreference()
        {
            ApplicationData.Current.LocalSettings.Values["CartSortOption"] = SortOption.ToString();
        }

        private void LoadSortPreference()
        {
            var setting = ApplicationData.Current.LocalSettings.Values["CartSortOption"] as string;
            if (Enum.TryParse(setting, out CartSortOption option))
            {
                _sortOption = option;
            }
            else
            {
                _sortOption = CartSortOption.ByName;
            }
        }
    }
}
