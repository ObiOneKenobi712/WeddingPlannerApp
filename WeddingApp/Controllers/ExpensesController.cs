using Microsoft.AspNetCore.Mvc;
using WeddingApp.DTOs;
using WeddingApp.Models;
using WeddingApp.Services;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService)
    {
        _expensesService = expensesService;
    }

    [HttpGet("{id}/expenses")]
    public ActionResult<IEnumerable<ExpenseModel>> GetExpenses(int id)
    {
        return Ok(_expensesService.GetAll(id));
    }

    [HttpPost("{id}/expenses")]
    public ActionResult AddExpense(int id, CreateExpenseDto dto)
    {
        var newId = _expensesService.Create(id, dto);
        return CreatedAtAction(nameof(GetExpenses), new { id, expenseId = newId }, dto);
    }

    [HttpPut("{id}/expenses/{expenseId}")]
    public ActionResult UpdateExpense(int id, int expenseId, UpdateExpenseDto dto)
    {
        _expensesService.Update(id, expenseId, dto);
        return NoContent();
    }

    [HttpDelete("{id}/expenses/{expenseId}")]
    public ActionResult RemoveExpense(int id, int expenseId)
    {
        _expensesService.Delete(id, expenseId);
        return NoContent();
    }
}