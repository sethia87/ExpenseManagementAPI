using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet("get-all-expenses")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _expenseService.GetAllExpenses());
    }

    //GET /api/expenses/get-by-date/2024-03-07
    [HttpGet("get-by-date/{dateString}")]
    public async Task<IActionResult> GetByDate(string dateString)
    {
        if (!DateTime.TryParse(dateString, out DateTime date))
            return BadRequest("Invalid date format. Use YYYY-MM-DD.");

        var expense = await _expenseService.GetExpenseByDate(date);
        if (expense == null) 
            return NotFound("Expense not found");

        return Ok(expense);
    }

    //GET /api/expenses/get-by-date-range?startDate=2024-03-01&endDate=2024-03-07
    [HttpGet("get-by-date-range")]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate == default || endDate == default)
            return BadRequest("Invalid date range. Please provide both startDate and endDate in YYYY-MM-DD format.");

        var expenses = await _expenseService.GetExpensesByDateRange(startDate, endDate);

        if (!expenses.Any())
            return NotFound("No expenses found in the given date range.");

        return Ok(expenses);
    }

    //GET /api/expenses/get-by-category/2
    [HttpGet("get-by-category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        if (categoryId <= 0)
            return BadRequest("Invalid category ID. It must be greater than 0.");

        var expenses = await _expenseService.GetExpensesByCategory(categoryId);

        if (!expenses.Any())
            return NotFound($"No expenses found for category ID {categoryId}.");

        return Ok(expenses);
    }

    //GET /api/expenses/get-by-amount-range?minAmount=100&maxAmount=1000
    [HttpGet("get-by-amount-range")]
    public async Task<IActionResult> GetByAmountRange([FromQuery] decimal minAmount, [FromQuery] decimal maxAmount)
    {
        if (minAmount < 0 || maxAmount <= 0 || minAmount > maxAmount)
            return BadRequest("Invalid amount range. Ensure minAmount >= 0, maxAmount > 0, and minAmount <= maxAmount.");

        var expenses = await _expenseService.GetExpensesByAmountRange(minAmount, maxAmount);

        if (!expenses.Any())
            return NotFound($"No expenses found in the amount range ₹{minAmount} - ₹{maxAmount}.");

        return Ok(expenses);
    }


    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] ExpenseDto expenseDto)
    {
        await _expenseService.AddExpense(expenseDto);
        return CreatedAtAction(nameof(GetAll), new { message = "Expense added successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDto expenseDto)
    {
        await _expenseService.UpdateExpense(id, expenseDto);
        return Ok("Expense updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await _expenseService.DeleteExpense(id);
        return Ok("Expense deleted successfully");
    }
}

