using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.DTO.TodoDTO
{
    public class TodoGetDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Changed from User to int for the foreign key ID
        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public int PriorityId { get; set; } // Changed from Priority to int
        public int StatusId { get; set; } // Changed from Status to int
        public int CategoryId { get; set; } // Changed from Category to int
        public string? ImageURL { get; set; }
        public DateTime Deadline { get; set; }
    }
}