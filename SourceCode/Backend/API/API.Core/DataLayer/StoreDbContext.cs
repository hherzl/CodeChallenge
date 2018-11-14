using API.Core.DataLayer.Configurations.Sales;
using API.Core.DataLayer.Configurations.Warehouse;
using API.Core.EntityLayer.Sales;
using API.Core.EntityLayer.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace API.Core.DataLayer
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPriceHistory> ProductPriceHistory { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations for tables

            modelBuilder
                .ApplyConfiguration(new OrderDetailConfiguration())
                .ApplyConfiguration(new OrderHeaderConfiguration())
                .ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new ProductPriceHistoryConfiguration())
            ;

            base.OnModelCreating(modelBuilder);
        }
    }
}
