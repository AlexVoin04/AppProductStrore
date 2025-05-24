using AppProductStrore.Helpers;
using AppProductStrore.Interfaces;
using AppProductStrore.Models;
using AppProductStrore.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }

        public StoreViewModel(IProductService productService)
        {
            _productService = productService;
            AddToCartCommand = new RelayCommand(async (product) => await AddToCartAsync(product as Product));
            IncreaseQuantityCommand = new RelayCommand(async (product) => await IncreaseQuantityAsync(product as Product));
            DecreaseQuantityCommand = new RelayCommand(async (product) => await DecreaseQuantityAsync(product as Product));
            Task.Run(async () => await LoadProductsAsync()).GetAwaiter().GetResult();
        }

        private async Task LoadProductsAsync()
        {
            Products = new ObservableCollection<Product>();
            var products = await _productService.LoadProductsAsync();
            var cartItems = await _cartService.LoadCartAsync();
            foreach (var product in products)
            {
                var inCart = cartItems.FirstOrDefault(c => c.Product.Id == product.Id);
                if (inCart != null)
                    product.QuantityInCart = inCart.Quantity;

                Products.Add(product);
            }
            Debug.WriteLine($"product count: {products.Count}");
        }

        private async Task AddToCartAsync(Product product)
        {
            if (product == null) return;
            await _cartService.AddToCartAsync(product);
        }

        private async Task IncreaseQuantityAsync(Product product)
        {
            if (product == null) return;
            product.QuantityInCart++;
            await _cartService.AddToCartAsync(product);
        }

        private async Task DecreaseQuantityAsync(Product product)
        {
            if (product == null || product.QuantityInCart <= 0) return;

            var cartItems = await _cartService.LoadCartAsync();
            var existing = cartItems.FirstOrDefault(c => c.Product.Id == product.Id);

            if (existing != null)
            {
                existing.Quantity--;
                if (existing.Quantity <= 0)
                {
                    cartItems.Remove(existing);
                    product.QuantityInCart = 0;
                }
                else
                {
                    product.QuantityInCart = existing.Quantity;
                }
                await _cartService.SaveCartAsync(cartItems);
            }
        }
    }
}
