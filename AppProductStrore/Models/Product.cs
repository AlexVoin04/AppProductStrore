using AppProductStrore.ViewModels;
using System.ComponentModel;

namespace AppProductStrore.Models
{
    public class Product : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string ImagePath => $"ms-appdata:///local/Images/{Image}";
        public string PriceFormatted => $"{Price:C}";

        private int _quantityInCart;
        public int QuantityInCart
        {
            get => _quantityInCart;
            set
            {
                if (_quantityInCart != value)
                {
                    _quantityInCart = value;
                    OnPropertyChanged(nameof(QuantityInCart));
                    OnPropertyChanged(nameof(IsInCart));
                }
            }
        }

        public bool IsInCart => QuantityInCart > 0;
    }
}
