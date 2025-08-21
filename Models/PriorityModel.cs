using System.ComponentModel.DataAnnotations;

public class Priority
{
    [Key]
    public int Id { get; set; }
    public string PriorityType { get; set; } = string.Empty;
}