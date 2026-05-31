using Microsoft.AspNetCore.Mvc;
using WeddingApp.Models;
using WeddingApp.DTOs;

namespace WeddingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeddingsController : ControllerBase
{
    private static List<WeddingModel> weddings = new()
    {
        new WeddingModel
        {
            Id = 1,
            BrideName = "Anna",
            GroomName = "Jan",
            Date = new DateTime(2027, 6, 15),
            Venue = "Hotel Victoria",
            IsActive = true
        },
        new WeddingModel
        {
            Id = 2,
            BrideName = "Maria",
            GroomName = "Piotr",
            Date = new DateTime(2027, 8, 20),
            Venue = "Pałac Jabłonna",
            IsActive = true
        }
    };
// Wedding 

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(weddings);
    }
    
    [HttpPost]
    public IActionResult Create(CreateWeddingDto dto)
    {
        var wedding = new WeddingModel
        {
            Id = weddings.Count + 1,
            BrideName = dto.BrideName,
            GroomName = dto.GroomName,
            Date = dto.Date,
            Venue = dto.Venue,
            IsActive = true
        };

        weddings.Add(wedding);

        return Ok(wedding);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        return Ok(wedding);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, CreateWeddingDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        wedding.BrideName = dto.BrideName;
        wedding.GroomName = dto.GroomName;
        wedding.Date = dto.Date;
        wedding.Venue = dto.Venue;

        return Ok(wedding);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        weddings.Remove(wedding);

        return NoContent();
    }
    
    //Guest
    
    
    [HttpGet("{id}/guests")]
    public IActionResult GetGuests(int id)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        return Ok(wedding.Guests);
    }
    
    [HttpPost("{id}/guests")]
    public IActionResult AddGuest(int id, CreateGuestDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = new GuestModel
        {
            Id = wedding.Guests.Count + 1,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsConfirmed = dto.IsConfirmed
        };

        wedding.Guests.Add(guest);

        return Ok(guest);
    }
    
    [HttpPut("{id}/guests/{guestId}")]
    public IActionResult UpdateGuest(int id, int guestId, CreateGuestDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId);

        if (guest == null)
        {
            return NotFound();
        }

        guest.FirstName = dto.FirstName;
        guest.LastName = dto.LastName;
        guest.IsConfirmed = dto.IsConfirmed;

        return Ok(guest);
    }
    
    [HttpDelete("{id}/guests/{guestId}")]
    public IActionResult RemoveGuest(int id, int guestId)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId);

        if (guest == null)
        {
            return NotFound();
        }

        wedding.Guests.Remove(guest);

        return NoContent();
    }

    [HttpGet("{id}/guests/{guestId}")]
    public IActionResult GetGuestById(int id, int guestId)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        var guest = wedding.Guests.FirstOrDefault(g => g.Id == guestId);

        if (guest == null)
        {
            return NotFound();
        }

        return Ok(guest);
    }
    
    // Expense
    
    //GET Expenses
    
        [HttpGet("{id}/expenses")]
    public IActionResult GetExpenses(int id)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

        if (wedding == null)
        {
            return NotFound();
        }

        return Ok(wedding.Expenses);
    }
    
    //POST Expense
    
    [HttpPost("{id}/expenses")]
    public IActionResult AddExpense(int id, CreateExpenseDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

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
    
    //PUT Expense
    
    [HttpPut("{id}/expenses/{expenseId}")]
    public IActionResult UpdateExpense(int id, int expenseId, CreateExpenseDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

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
    
    //DELETE Expense
 
    [HttpDelete("{id}/expenses/{expenseId}")]
    public IActionResult RemoveExpense(int id, int expenseId)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

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
    
    // Budget
    
    //POST
    
    [HttpPost("{id}/budget")]
    public IActionResult CreateBudget(int id, CreateBudgetDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

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
    
    //GET
    
    [HttpGet("{id}/budget")]
    public IActionResult GetBudget(int id)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

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
    
    //PUT
    
    [HttpPut("{id}/budget")]
    public IActionResult UpdateBudget(int id, CreateBudgetDto dto)
    {
        var wedding = weddings.FirstOrDefault(w => w.Id == id);

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