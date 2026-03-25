using AutoSpaceTestTask.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoSpaceTestTask.Database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreSchedule> StoreSchedules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }

    }
}
