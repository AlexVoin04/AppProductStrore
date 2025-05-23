namespace AppProductStrore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string ImagePath => $"ms-appdata:///local/Images/{Image}";
        public string PriceFormatted => $"{Price:C}";
    }
}
