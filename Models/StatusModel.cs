using System.ComponentModel.DataAnnotations;

public class Status
{
    [Key]
    public int Id { get; set; }
    public string StatusName { get; set; }
}