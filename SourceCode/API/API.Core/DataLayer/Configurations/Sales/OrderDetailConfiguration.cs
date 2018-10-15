using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.Core.EntityLayer.Sales;

namespace API.Core.DataLayer.Configurations.Sales
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			// Set configuration for entity
			builder.ToTable("OrderDetail", "Sales");
			
			// Set key for entity
			builder.HasKey(p => p.OrderDetailID);
			
			// Set identity for entity (auto increment)
			builder.Property(p => p.OrderDetailID).UseSqlServerIdentityColumn();
			
			// Set configuration for columns
			builder.Property(p => p.OrderDetailID).HasColumnType("int").IsRequired();
			builder.Property(p => p.OrderHeaderID).HasColumnType("int").IsRequired();
			builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
			builder.Property(p => p.ProductName).HasColumnType("nvarchar(200)").IsRequired();
			builder.Property(p => p.UnitPrice).HasColumnType("decimal(8, 4)").IsRequired();
			builder.Property(p => p.Quantity).HasColumnType("int").IsRequired();
			builder.Property(p => p.Total).HasColumnType("decimal(12, 4)").IsRequired();
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
			
			// Add configuration for foreign keys
			builder
				.HasOne(p => p.OrderHeaderFk)
				.WithMany(b => b.OrderDetails)
				.HasForeignKey(p => p.OrderHeaderID)
				.HasConstraintName("FK_Sales_OrderDetail_Sales_OrderHeader");
			
			builder
				.HasOne(p => p.ProductFk)
				.WithMany(b => b.OrderDetails)
				.HasForeignKey(p => p.ProductID)
				.HasConstraintName("FK_Sales_OrderDetail_Warehouse_Product");
			
		}
	}
}
