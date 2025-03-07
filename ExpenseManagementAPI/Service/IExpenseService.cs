public interface IExpenseService
{
    Task<IEnumerable<ExpenseResponseDto>> GetAllExpenses();
    Task<ExpenseResponseDto> GetExpenseById(int id);
    Task AddExpense(ExpenseDto expenseDto);
    Task UpdateExpense(int id, ExpenseDto expenseDto);
    Task DeleteExpense(int id);
}


