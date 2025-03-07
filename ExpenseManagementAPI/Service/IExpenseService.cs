public interface IExpenseService
{
    Task<IEnumerable<ExpenseResponseDto>> GetAllExpenses();
    Task<IEnumerable<ExpenseResponseDto>> GetExpenseByDate(DateTime date);
    Task<IEnumerable<ExpenseResponseDto>> GetExpensesByDateRange(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ExpenseResponseDto>> GetExpensesByAmountRange(decimal minAmount, decimal maxAmount);
    Task<IEnumerable<ExpenseResponseDto>> GetExpensesByCategory(int categoryId);
    Task AddExpense(ExpenseDto expenseDto);
    Task UpdateExpense(int id, ExpenseDto expenseDto);
    Task DeleteExpense(int id);
}


