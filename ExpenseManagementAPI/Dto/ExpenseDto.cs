public class ExpenseDto
{
    public int UserID { get; set; }
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}

