using Microsoft.EntityFrameworkCore;
using WeddingApp.Models;

namespace WeddingApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WeddingModel> Weddings => Set<WeddingModel>();
    public DbSet<GuestModel> Guests => Set<GuestModel>();
    public DbSet<ExpenseModel> Expenses => Set<ExpenseModel>();
    public DbSet<BudgetModel> Budgets => Set<BudgetModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Filtr logicznego usuwania dla głównej encji
        modelBuilder.Entity<WeddingModel>().HasQueryFilter(w => !w.IsDeleted);

        modelBuilder.Entity<WeddingModel>()
            .HasMany(w => w.Guests)
            .WithOne()
            .HasForeignKey(g => g.WeddingModelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeddingModel>()
            .HasMany(w => w.Expenses)
            .WithOne()
            .HasForeignKey(e => e.WeddingModelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeddingModel>()
            .HasOne(w => w.Budget)
            .WithOne()
            .HasForeignKey<BudgetModel>(b => b.WeddingModelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

