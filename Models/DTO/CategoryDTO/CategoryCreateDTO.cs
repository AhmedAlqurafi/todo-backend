using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.CategoryDTO
{
    public class CategoryCreateDTO
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public int UserId { get; set; }
    }
}