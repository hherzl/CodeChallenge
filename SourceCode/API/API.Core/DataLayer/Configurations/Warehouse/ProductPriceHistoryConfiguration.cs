using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.DataLayer.Configurations.Warehouse
{
	public class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
	{
		public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)
		{
			// Set configuration for entity
			builder.ToTable("ProductPriceHistory", "Warehouse");
			
			// Set key for entity
			builder.HasKey(p => p.ProductID);
			
			// Set identity for entity (auto increment)
			builder.Property(p => p.ProductPriceHistoryID).UseSqlServerIdentityColumn();
			
			// Set configuration for columns
			builder.Property(p => p.ProductPriceHistoryID).HasColumnType("int").IsRequired();
			builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
			builder.Property(p => p.Price).HasColumnType("decimal(8, 4)").IsRequired();
			builder.Property(p => p.StartDate).HasColumnType("datetime").IsRequired();
			builder.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
			builder.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
			builder.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
			builder.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");
			builder.Property(p => p.Timestamp).HasColumnType("timestamp");
			
			// Set concurrency token for entity
			builder
				.Property(p => p.Timestamp)
				.ValueGeneratedOnAddOrUpdate()
				.IsConcurrencyToken();
			
		}
	}
}
