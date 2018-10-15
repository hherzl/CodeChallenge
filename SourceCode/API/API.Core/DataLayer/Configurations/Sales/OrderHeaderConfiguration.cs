using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using API.Core.EntityLayer.Sales;

namespace API.Core.DataLayer.Configurations.Sales
{
	public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
	{
		public void Configure(EntityTypeBuilder<OrderHeader> builder)
		{
			// Set configuration for entity
			builder.ToTable("OrderHeader", "Sales");
			
			// Set key for entity
			builder.HasKey(p => p.OrderHeaderID);
			
			// Set identity for entity (auto increment)
			builder.Property(p => p.OrderHeaderID).UseSqlServerIdentityColumn();
			
			// Set configuration for columns
			builder.Property(p => p.OrderHeaderID).HasColumnType("int").IsRequired();
			builder.Property(p => p.OrderDate).HasColumnType("datetime").IsRequired();
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
			
		}
	}
}
