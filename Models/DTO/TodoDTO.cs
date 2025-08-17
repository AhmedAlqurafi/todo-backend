using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TodoDTO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("Id")]
    public User UserId { get; set; }
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Details { get; set; } = "";
    [ForeignKey("Id")]
    public Priority Priority { get; set; }
    [ForeignKey("Id")]
    public Status Status { get; set; }
    [ForeignKey("Id")]
    public Category Category { get; set; }
    public string? ImageURL { get; set; }
    [Required]
    public DateTime Deadline { get; set; }
}