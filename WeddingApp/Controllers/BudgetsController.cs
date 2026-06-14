using Microsoft.AspNetCore.Mvc;
using WeddingApp.DTOs;
using WeddingApp.Models;
using WeddingApp.Services;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetsService _budgetsService;

    public BudgetsController(IBudgetsService budgetsService)
    {
        _budgetsService = budgetsService;
    }

    [HttpPost("{id}/budget")]
    public ActionResult<BudgetModel> CreateBudget(int id, CreateBudgetDto dto)
    {
        var created = _budgetsService.Create(id, dto);
        return CreatedAtAction(nameof(GetBudget), new { id }, created);
    }

    [HttpGet("{id}/budget")]
    public ActionResult<BudgetModel> GetBudget(int id)
    {
        return Ok(_budgetsService.Get(id));
    }

    [HttpPut("{id}/budget")]
    public ActionResult<BudgetModel> UpdateBudget(int id, UpdateBudgetDto dto)
    {
        var updated = _budgetsService.Update(id, dto);
        return Ok(updated);
    }

}