using System.ComponentModel.DataAnnotations;

public class Priority
{
    [Key]
    public int Id { get; set; }
    public string PriorityType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } 
}