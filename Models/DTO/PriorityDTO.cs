using System.ComponentModel.DataAnnotations;

public class PriorityDTO
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string PriorityType { get; set; }
}