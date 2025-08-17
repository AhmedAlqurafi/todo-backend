using System.ComponentModel.DataAnnotations;

public class StatusDTO
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string StatusName { get; set; }
}