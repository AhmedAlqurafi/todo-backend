using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.DTO.TodoDTO
{
    public class TodoUpdateDTO
    {
        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public int PriorityId { get; set; } // Added for better client consumption
        public int StatusId { get; set; } // Added for better client consumption
        public int CategoryId { get; set; } // Added for better client consumption
        public string? ImageURL { get; set; }
        public DateTime Deadline { get; set; }
    }
}