using Microsoft.AspNetCore.Mvc;
using WeddingApp.Models;
using WeddingApp.DTOs;
using WeddingApp.Data;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    
    [HttpPost("{id}/budget")]
    public IActionResult CreateBudget(int id, CreateBudgetDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        wedding.Budget = new BudgetModel
        {
            TotalBudget = dto.TotalBudget,
            Spent = 0,
            Remaining = dto.TotalBudget
        };

        return Ok(wedding.Budget);
    }
    
    
    [HttpGet("{id}/budget")]
    public IActionResult GetBudget(int id)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        if (wedding.Budget == null)
        {
            return NotFound("Budget not set.");
        }

        return Ok(wedding.Budget);
    }
    
    
    [HttpPut("{id}/budget")]
    public IActionResult UpdateBudget(int id, CreateBudgetDto dto)
    {
        var wedding = WeddingData.Weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        if (wedding.Budget == null)
        {
            return NotFound("Budget not set.");
        }

        wedding.Budget.TotalBudget = dto.TotalBudget;
        wedding.Budget.Remaining = wedding.Budget.TotalBudget - wedding.Budget.Spent;

        return Ok(wedding.Budget);
    }

}