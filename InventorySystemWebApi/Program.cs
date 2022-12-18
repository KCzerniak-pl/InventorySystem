using Database;
using Database.Entities.User;
using InventorySystemWebApi.Intefaces;
using InventorySystemWebApi.Jwt;
using InventorySystemWebApi.Middleware;
using InventorySystemWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(c => c.LowercaseUrls = true);
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory System API", Version = "v1" });

    // JWT - config for Swagger.
    c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
        In = ParameterLocation.Header,
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    // JWT - operation filter for Swagger.
    c.OperationFilter<AuthResponsesOperationFilterForSwagger>();
});

// Context for database connection (required references to the library "Database").
string? connectonString = builder.Configuration.GetConnectionString(name: "InventorySystemDatabase");
builder.Services.AddDbContext<InventorySystemDbContext>(opt => opt.UseSqlServer(connectonString));

// JWT - Get JWT configuration from "appsettings.json" and mapping this to "JwtConfig" object.
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

// JWT - Validate token.
JwtAuthenticationExtension.AddAuthentication(builder.Services);

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
