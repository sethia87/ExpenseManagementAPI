using Microsoft.EntityFrameworkCore;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    // Get all expenses with category names
    public async Task<IEnumerable<ExpenseResponseDto>> GetAllExpenses()
    {
        return await _context.Expenses
            //.Include(e => e.Category)
            .Select(e => new ExpenseResponseDto
            {
                ID = e.ID,
                UserID = e.UserID,
                Amount = e.Amount,
                CategoryName = e.Category.Name,
                Description = e.Description,
                Date = e.Date
            })
            .ToListAsync();
    }

    // Get expense by ID
    public async Task<ExpenseResponseDto> GetExpenseById(int id)
    {
        var expense = await _context.Expenses
            //.Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.ID == id);

        if (expense == null)
            throw new Exception("Expense not found");

        return new ExpenseResponseDto
        {
            ID = expense.ID,
            UserID = expense.UserID,
            Amount = expense.Amount,
            CategoryName = expense.Category.Name,
            Description = expense.Description,
            Date = expense.Date
        };
    }

    // Add new expense
    public async Task AddExpense(ExpenseDto expenseDto)
    {
        // Ensure category exists
        var categoryExists = await _context.Categories.AnyAsync(c => c.ID == expenseDto.CategoryId);
        if (!categoryExists)
            throw new Exception("Category does not exist");

        var expense = new Expense
        {
            Amount = expenseDto.Amount,
            CategoryId = expenseDto.CategoryId,
            Category =  await _context.Categories.FindAsync(expenseDto.CategoryId),
            Description = expenseDto.Description,
            Date = expenseDto.Date
        };

        await _context.Expenses.AddAsync(expense);
        await _context.SaveChangesAsync();
    }

    // Update existing expense
    public async Task UpdateExpense(int id, ExpenseDto expenseDto)
    {
        var existingExpense = await _context.Expenses.FindAsync(id);
        if (existingExpense == null)
            throw new Exception("Expense not found");

        existingExpense.Amount = expenseDto.Amount;
        existingExpense.CategoryId = expenseDto.CategoryId;
        existingExpense.Description = expenseDto.Description;
        existingExpense.Date = expenseDto.Date;

        _context.Expenses.Update(existingExpense);
        await _context.SaveChangesAsync();
    }

    // Delete expense
    public async Task DeleteExpense(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
            throw new Exception("Expense not found");

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
    }
}


