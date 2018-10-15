using API.Core.DataLayer.Configurations.Sales;
using API.Core.DataLayer.Configurations.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace API.Core.DataLayer
{
    public class CodeChallengeDbContext : DbContext
    {
        public CodeChallengeDbContext(DbContextOptions<CodeChallengeDbContext> options)
            : base(options)
        {
        }

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
