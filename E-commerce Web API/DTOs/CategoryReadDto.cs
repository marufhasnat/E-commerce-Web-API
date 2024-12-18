namespace E_commerce_Web_API.DTOs
{
    public class CategoryReadDto
    {
        // Things I want to return
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
