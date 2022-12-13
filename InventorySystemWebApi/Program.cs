using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Middleware;
using InventorySystemWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(c => c.LowercaseUrls = true);
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory System API", Version = "v1" });
});

// Context for database connection (required references to the library "Database").
string? connectonString = builder.Configuration.GetConnectionString(name: "InventorySystemDatabase");
builder.Services.AddDbContext<Database.InventorySystemDbContext>(opt => opt.UseSqlServer(connectonString));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
