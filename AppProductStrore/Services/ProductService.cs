using AppProductStrore.Interfaces;
using AppProductStrore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppProductStrore.Services
{
    public class ProductService : IProductService
    {
        public async Task<ObservableCollection<Product>> LoadProductsAsync()
        {
            var folder = ApplicationData.Current.LocalFolder;
            var jsonFolder = await folder.GetFolderAsync("Data");
            var file = await jsonFolder.GetFileAsync("products.json");
            string json = await FileIO.ReadTextAsync(file);
            return JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
        }
    }
}
