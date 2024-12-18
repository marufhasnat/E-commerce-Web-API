using System.ComponentModel.DataAnnotations;

namespace E_commerce_Web_API.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength =5, ErrorMessage ="Category name must be between 5 to 100 characters.")]
        public string? CategoryName { get; set; }
        
        [StringLength(500, ErrorMessage = "Category description must be can't exceed 500 characters.")]
        public string? CategoryDescription { get; set; }
    }
}
