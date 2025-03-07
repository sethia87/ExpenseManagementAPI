using System.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    // Get all expenses 
    public async Task<IEnumerable<ExpenseResponseDto>> GetAllExpenses()
    {
        return await _context.Expenses
        .Include(e => e.Category) // Ensure Category data is loaded
        .Select(e => new ExpenseResponseDto
        {
            ID = e.ID,
            Amount = e.Amount,
            Description = e.Description,
            Date = e.Date,
            CategoryId = e.CategoryId,
            CategoryName = e.Category.Name
        })
        .ToListAsync();

    }

    // Get expense by Date
    public async Task<IEnumerable<ExpenseResponseDto>> GetExpenseByDate(DateTime date)
    {
        return await _context.Expenses
              .Where(e => e.Date == date)
              .Include(e => e.Category)
              .Select(e => new ExpenseResponseDto
              {
                  ID = e.ID,
                  //Name = e.Name,
                  Amount = e.Amount,
                  Description = e.Description,
                  Date = e.Date,
                  CategoryName = e.Category.Name
              })
              .ToListAsync();
    }

    // Get Expenses by Date Range
    public async Task<IEnumerable<ExpenseResponseDto>> GetExpensesByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Expenses
              .Where(e => e.Date >= startDate && e.Date <= endDate)
              .Include(e => e.Category)
              .Select(e => new ExpenseResponseDto
              {
                  ID = e.ID,
                  //Name = e.Name,
                  Amount = e.Amount,
                  Description = e.Description,
                  Date = e.Date,
                  CategoryName = e.Category.Name
              })
              .ToListAsync();
    }

    // Get All expenses by category
    public async Task<IEnumerable<ExpenseResponseDto>> GetExpensesByCategory(int categoryId)
    {
        return await _context.Expenses
            .Where(e => e.CategoryId == categoryId)
            .Include(e => e.Category)
            .Select(e => new ExpenseResponseDto
            {
                ID = e.ID,
                //Name = e.Name,
                Amount = e.Amount,
                Description = e.Description,
                Date = e.Date,
                CategoryName = e.Category.Name
            })
            .ToListAsync();
    }

    // Get Expenses by Amount Range
    public async Task<IEnumerable<ExpenseResponseDto>> GetExpensesByAmountRange(decimal minAmount, decimal maxAmount)
    {
        return await _context.Expenses
            .Where(e => e.Amount >= minAmount && e.Amount <= maxAmount)
            .OrderBy(e => e.Amount)
            .Include(e => e.Category)
            .Select(e => new ExpenseResponseDto
            {
                ID = e.ID,
                //Name = e.Name,
                Amount = e.Amount,
                Description = e.Description,
                Date = e.Date,
                CategoryName = e.Category.Name
            })
            .ToListAsync();
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
            Name= expenseDto.Name,
            Amount = expenseDto.Amount,
            CategoryId = expenseDto.CategoryId,
            Description = expenseDto.Description,
            Date = expenseDto.Date,
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


