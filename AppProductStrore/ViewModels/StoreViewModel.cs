using AppProductStrore.Interfaces;
using AppProductStrore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProductStrore.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public StoreViewModel(IProductService productService)
        {
            _productService = productService;
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
    }
}
