using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}
