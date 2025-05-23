using AppProductStrore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProductStrore.Interfaces
{
    public interface IProductService
    {
        Task<ObservableCollection<Product>> LoadProductsAsync();
    }
}
