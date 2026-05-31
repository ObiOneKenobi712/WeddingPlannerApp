using Microsoft.AspNetCore.Mvc;
using WeddingApp.Models;
using WeddingApp.DTOs;
using WeddingApp.Data;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    [HttpGet("{id}/expenses")]
    public IActionResult GetExpenses(int id)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        return Ok(wedding.Expenses);
    }
    
    
    [HttpPost("{id}/expenses")]
    public IActionResult AddExpense(int id, CreateExpenseDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var expense = new ExpenseModel
        {
            Id = wedding.Expenses.Count + 1,
            Name = dto.Name,
            Cost = dto.Cost
        };

        wedding.Expenses.Add(expense);

        if (wedding.Budget != null)
        {
            wedding.Budget.Spent += expense.Cost;
            wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
        }

        return Ok(expense);
    }
    
    
    [HttpPut("{id}/expenses/{expenseId}")]
    public IActionResult UpdateExpense(int id, int expenseId, CreateExpenseDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var expense = wedding.Expenses.FirstOrDefault(e => e.Id == expenseId);

        if (expense == null)
        {
            return NotFound();
        }

        var oldCost = expense.Cost;

        expense.Name = dto.Name;
        expense.Cost = dto.Cost;

        if (wedding.Budget != null)
        {
            wedding.Budget.Spent = wedding.Budget.Spent - oldCost + expense.Cost;
            wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
        }

        return Ok(expense);
    }
    
    
    [HttpDelete("{id}/expenses/{expenseId}")]
    public IActionResult RemoveExpense(int id, int expenseId)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var expense = wedding.Expenses.FirstOrDefault(e => e.Id == expenseId);

        if (expense == null)
        {
            return NotFound();
        }

        if (wedding.Budget != null)
        {
            wedding.Budget.Spent -= expense.Cost;
            wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;
        }

        wedding.Expenses.Remove(expense);

        return NoContent();
    }
    
}