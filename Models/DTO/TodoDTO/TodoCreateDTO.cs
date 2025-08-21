using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.DTO.TodoDTO
{
    public class TodoCreateDTO
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        [Required]
        public string Details { get; set; } = "";
        [Required]
        public string PriorityName { get; set; } = ""; // Added for better client consumption
        [Required]
        public string StatusName { get; set; } = ""; // Added for better client consumption
        [Required]
        public string CategoryName { get; set; } = ""; // Added for better client consumption
        public string? ImageURL { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
    }
}