using Database.Entities.User;
using Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace InventorySystemWebApi.Tests.Integration
{
    public class InwentorySystemWebApiFactory
    {
        private readonly WebApplicationFactory<Program> _factory;
        public HttpClient Client { get; }

        public InwentorySystemWebApiFactory()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => // Override service for DbContect.
                    {
                        // Get service for DbContext (for SqlServer).
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<InventorySystemDbContext>));

                        // Remove service.
                        services.Remove(dbContextOptions!);

                        // Add new service for DbContect (for in-memory database).
                        services.AddDbContext<InventorySystemDbContext>(options => options.UseInMemoryDatabase(databaseName: "InventorySystemDbContext"));
                    });
                });

            Client = _factory.CreateClient();

            Seed();
        }

        private void Seed()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "User"
                }
            };

            var user = new User()
            {
                Email = "oliver.smith@test.com",
                FirstName = "Oliver",
                LastName = "Smith",
                PasswordHash = "ADWOD9UYMXPvBYLxvY7zOob3kWSou6lvobTY/koHsJuCLsuVZa+nNRA7f6mKETbDOg==",
                RoleId = 1
            };

            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory!.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<InventorySystemDbContext>();

            dbContext!.Roles.AddRange(roles);
            dbContext!.Users.Add(user);
            dbContext.SaveChanges();
        }
    }
}
