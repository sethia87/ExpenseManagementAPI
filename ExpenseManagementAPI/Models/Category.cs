public class Category
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public List<Expense> Expenses { get; set; }
}
