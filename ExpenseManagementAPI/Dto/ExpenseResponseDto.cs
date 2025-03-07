using System.ComponentModel.DataAnnotations;
public class ExpenseResponseDto
{
    public int ID { get; set; }
    public decimal Amount { get; set; }
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; } // Ensure this exists
}

