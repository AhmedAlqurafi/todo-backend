using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.DTO.TodoDTO
{
    public class TodoUpdateDTO
    {
        public string Title { get; set; } = "";
        public string Details { get; set; } = "";
        public string PriorityName { get; set; } = ""; // Added for better client consumption
        public string StatusName { get; set; } = ""; // Added for better client consumption
        public string CategoryName { get; set; } = ""; // Added for better client consumption
        public string? ImageURL { get; set; }
        public DateTime Deadline { get; set; }
    }
}