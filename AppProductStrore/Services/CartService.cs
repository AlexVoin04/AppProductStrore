using AppProductStrore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppProductStrore.Services
{
    public class CartService
    {
        private const string CartFolder = "Data";
        private const string CartFileName = "cart.json";
        private readonly StorageFolder _localFolder = ApplicationData.Current.LocalFolder;

        public async Task<ObservableCollection<CartItem>> LoadCartAsync()
        {
            try
            {
                var folder = await _localFolder.CreateFolderAsync(CartFolder, CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync(CartFileName, CreationCollisionOption.OpenIfExists);
                var json = await FileIO.ReadTextAsync(file);

                return string.IsNullOrWhiteSpace(json)
                    ? new ObservableCollection<CartItem>()
                    : JsonConvert.DeserializeObject<ObservableCollection<CartItem>>(json);
            }
            catch
            {
                return new ObservableCollection<CartItem>();
            }
        }

        public async Task SaveCartAsync(ObservableCollection<CartItem> cartItems)
        {
            var folder = await _localFolder.CreateFolderAsync(CartFolder, CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync(CartFileName, CreationCollisionOption.ReplaceExisting);
            var json = JsonConvert.SerializeObject(cartItems, Formatting.Indented);
            await FileIO.WriteTextAsync(file, json);
        }

        public async Task AddToCartAsync(Product product)
        {
            var cartItems = await LoadCartAsync();
            var existing = cartItems.FirstOrDefault(c => c.Product.Id == product.Id);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }

            await SaveCartAsync(cartItems);
        }
    }
}
