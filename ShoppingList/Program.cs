using Microsoft.EntityFrameworkCore;
using MovieStoreApi.Middlewares;
using MovieStoreApi.Services;
using ShoppingList.DbOperations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShoppingListDbContext>(options => options.UseInMemoryDatabase(databaseName: "ShoppingListDB"));
builder.Services.AddScoped<IShoppingListDbContext>(c => c.GetService<ShoppingListDbContext>());

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();

