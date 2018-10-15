using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.DataLayer.Configurations.Warehouse
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			// Set configuration for entity
			builder.ToTable("Product", "Warehouse");
			
			// Set key for entity
			builder.HasKey(p => p.ProductID);
			
			// Set identity for entity (auto increment)
			builder.Property(p => p.ProductID).UseSqlServerIdentityColumn();
			
			// Set configuration for columns
			builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
			builder.Property(p => p.ProductName).HasColumnType("nvarchar(200)").IsRequired();
			builder.Property(p => p.ProductDescription).HasColumnType("nvarchar(max)");
			builder.Property(p => p.Price).HasColumnType("decimal(8, 4)").IsRequired();
			builder.Property(p => p.Likes).HasColumnType("int").IsRequired();
			builder.Property(p => p.Stocks).HasColumnType("int").IsRequired();
			builder.Property(p => p.Available).HasColumnType("bit").IsRequired();
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
			
			// Add configuration for uniques
			builder
				.HasIndex(p => p.ProductName)
				.IsUnique()
				.HasName("U_Warehouse_Product_ProductName");
			
		}
	}
}
