using Microsoft.EntityFrameworkCore;
using WeddingApp.Data;
using WeddingApp.Middleware;
using WeddingApp.Models;
using WeddingApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IWeddingsService, WeddingsService>();
builder.Services.AddScoped<IGuestsService, GuestsService>();
builder.Services.AddScoped<IExpensesService, ExpensesService>();
builder.Services.AddScoped<IBudgetsService, BudgetsService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Weddings.Any())
    {
        context.Weddings.AddRange(
            new WeddingModel
            {
                BrideName = "Anna",
                GroomName = "Jan",
                Date = new DateTime(2027, 6, 15),
                Venue = "Hotel Victoria",
                IsActive = true,
                IsDeleted = false
            },
            new WeddingModel
            {
                BrideName = "Maria",
                GroomName = "Piotr",
                Date = new DateTime(2027, 8, 20),
                Venue = "Palac Jablonna",
                IsActive = true,
                IsDeleted = false
            }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();