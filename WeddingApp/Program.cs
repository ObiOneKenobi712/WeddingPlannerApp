using WeddingApp.Middleware;
using WeddingApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IWeddingsService, WeddingsService>();
builder.Services.AddSingleton<IGuestsService, GuestsService>();
builder.Services.AddSingleton<IExpensesService, ExpensesService>();
builder.Services.AddSingleton<IBudgetsService, BudgetsService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();