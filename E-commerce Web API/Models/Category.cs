namespace E_commerce_Web_API.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
