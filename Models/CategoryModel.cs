using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
   public DateTime UpdatedAt { get; set; } 
}