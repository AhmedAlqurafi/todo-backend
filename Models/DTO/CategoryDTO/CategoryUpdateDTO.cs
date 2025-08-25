using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO.CategoryDTO
{
    public class CategoryUpdateDTO
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;
    }
}