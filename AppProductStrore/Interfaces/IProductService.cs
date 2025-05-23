using AppProductStrore.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppProductStrore.Interfaces
{
    public interface IProductService
    {
        Task<ObservableCollection<Product>> LoadProductsAsync();
    }
}
