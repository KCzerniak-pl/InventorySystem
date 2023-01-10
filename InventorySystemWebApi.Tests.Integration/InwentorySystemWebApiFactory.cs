using Database.Entities.User;
using Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization.Policy;
using Database.Entities.Item;

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
                    builder.ConfigureServices(services =>
                    {
                        // Get service for DbContext (for SqlServer).
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<InventorySystemDbContext>));

                        // Remove service.
                        services.Remove(dbContextOptions!);

                        // Add new service for DbContect (for in-memory database).
                        services.AddDbContext<InventorySystemDbContext>(options => options.UseInMemoryDatabase(databaseName: "InventorySystemDbContext"));

                        // Add new serivce for policy evaluator.
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                        // Add new register filter.
                        services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));
                    });
                });

            Client = _factory.CreateClient();

            Seed();
        }

        private void Seed()
        {
            // User.
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

            // Item.
            var type = new Database.Entities.Item.Type
            {
                Name = "Type Test"
            };

            var group = new Group()
            {
                Name = "Type Test"
            };

            var location = new Location()
            {
                Name = "Location Test"
            };

            var items = new List<Item>()
            {
                new Item()
                {
                    Name = "Item Test 1",
                    TypeId = 1,
                    GroupId = 1,
                    InventoryNumber = "111111111",
                    LocationId = 1
                },
                new Item()
                {
                    Name = "Item Test 2",
                    TypeId = 1,
                    GroupId = 1,
                    InventoryNumber = "111111112",
                    LocationId = 1
                }
            };

            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory!.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<InventorySystemDbContext>();

            dbContext!.Database.EnsureDeleted();
            dbContext!.Roles.AddRange(roles);
            dbContext!.Users.Add(user);
            dbContext!.Types.Add(type);
            dbContext!.Groups.Add(group);
            dbContext!.Locations.Add(location);
            dbContext!.Items.AddRange(items);
            dbContext.SaveChanges();
        }
    }
}
