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
        public int PriorityId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? ImageURL { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
    }
}