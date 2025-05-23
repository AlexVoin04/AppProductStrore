using AppProductStrore.Helpers;
using AppProductStrore.Interfaces;
using AppProductStrore.Models;
using AppProductStrore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppProductStrore.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        private readonly CartService _cartService = new CartService();
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public ICommand AddToCartCommand { get; }

        public StoreViewModel(IProductService productService)
        {
            _productService = productService;
            AddToCartCommand = new RelayCommand(async (product) => await AddToCartAsync(product as Product));
            Task.Run(async () => await LoadProductsAsync()).GetAwaiter().GetResult();
        }

        private async Task LoadProductsAsync()
        {
            Products = new ObservableCollection<Product>();
            var products = await _productService.LoadProductsAsync();
            foreach (var product in products)
                Products.Add(product);
            Debug.WriteLine($"product count: {products.Count}");
        }

        private async Task AddToCartAsync(Product product)
        {
            if (product == null) return;
            await _cartService.AddToCartAsync(product);
        }
    }
}
