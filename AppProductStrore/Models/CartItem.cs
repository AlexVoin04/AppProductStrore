using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppProductStrore.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => Product.Price * Quantity;

        public string PriceFormatted => $"Цена: {Product.Price:C}";
        public string QuantityFormatted => $"Количество: {Quantity}";
        public string TotalFormatted => $"Сумма: {TotalPrice:C}";
    }
}
