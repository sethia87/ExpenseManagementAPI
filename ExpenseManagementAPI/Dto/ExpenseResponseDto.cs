public class ExpenseResponseDto
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public decimal Amount { get; set; }
    public required string CategoryName { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}

