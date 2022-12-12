using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class InventorySystemDbContext : DbContext
    {
        public InventorySystemDbContext(DbContextOptions options) : base(options) { }

        // Properties DbSet for entity that is mapped on database table and view.
        // DbSet represents the entity set which can use for create, read, update and delete data in database.
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Entities.Type> Types { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
    }
}