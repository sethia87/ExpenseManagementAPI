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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _expenseService.GetAllExpenses());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var expense = await _expenseService.GetExpenseById(id);
        if (expense == null) 
            return NotFound("Expense not found");

        return Ok(expense);
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

