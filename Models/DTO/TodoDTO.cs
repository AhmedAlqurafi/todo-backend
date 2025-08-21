using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// public class TodoDTO
// {
//     [Key]
//     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//     public int Id { get; set; }
//     [ForeignKey("Id")]
//     public User UserId { get; set; }
//     [Required]
//     public string Title { get; set; } = "";
//     [Required]
//     public string Details { get; set; } = "";
//     [Required]
//     public int PriorityId { get; set; }
//     [ForeignKey("Id")]
//     public Priority Priority { get; set; }

//     [Required]
//     public int StatusId { get; set; }

//     [ForeignKey("Id")]
//     public Status Status { get; set; }

//     [Required]
//     public int CategoryId { get; set; }
//     [ForeignKey("Id")]
//     public Category Category { get; set; }
//     public string? ImageURL { get; set; }
//     [Required]
//     public DateTime Deadline { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public DateTime UpdatedAt { get; set; }
// }
namespace backend.Migrations.DTO
{


    public class TodoDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Details { get; set; } = "";

        [Required]
        public int PriorityId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? ImageURL { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}