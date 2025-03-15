namespace EShop.Models
{
    public class Category:BaseModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string country { get; set; } = "Poland";
        public string size { get; set; }= "Medium";
        public bool eco { get; set; } = true;

    }
}
