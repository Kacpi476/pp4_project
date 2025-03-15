namespace EShop.Models
{
    public class Product : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string ean { get; set; } = default!;

        public decimal price { get; set; }

        public int stock { get; set; }

        public string sku { get; set; } = default!;

        public Category category { get; set; } = default!;
    }
}
