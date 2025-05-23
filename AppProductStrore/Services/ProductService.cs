using AppProductStrore.Interfaces;
using AppProductStrore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace AppProductStrore.Services
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Asynchronously loads a list of products from the local "products.json" file
        /// </summary>
        /// <returns>
        /// A list of <see cref="Product"/> objects read from the file.
        /// Returns <c>null</c> if the file is empty or contains invalid data.
        /// </returns>
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
