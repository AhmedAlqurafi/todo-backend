using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Todo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    // [ForeignKey("UserId")]
    // public User User { get; set; }

    public string Title { get; set; } = "";
    public string Details { get; set; } = "";

    public int PriorityId { get; set; }
    // [ForeignKey("PriorityId")]
    // public Priority Priority { get; set; }

    public int StatusId { get; set; }
    // [ForeignKey("StatusId")]
    // public Status Status { get; set; }

    public int CategoryId { get; set; }
    // [ForeignKey("CategoryId")]
    // public Category Category { get; set; }

    public string? ImageURL { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}