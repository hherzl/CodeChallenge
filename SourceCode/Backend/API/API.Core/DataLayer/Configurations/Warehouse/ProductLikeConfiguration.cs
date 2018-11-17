using API.Core.EntityLayer.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Core.DataLayer.Configurations.Warehouse
{
    public class ProductLikeConfiguration : IEntityTypeConfiguration<ProductLike>
    {
        public void Configure(EntityTypeBuilder<ProductLike> builder)
        {
            // Set configuration for entity
            builder.ToTable("ProductLike", "Warehouse");

            // Set key for entity
            builder.HasKey(p => p.ProductLikeID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ProductLikeID).UseSqlServerIdentityColumn();

            // Set configuration for columns
            builder.Property(p => p.ProductID).HasColumnType("int").IsRequired();
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

            // Set unique key for entity
            builder
                .HasIndex(p => new { p.ProductID, p.CreationUser })
                .IsUnique()
                .HasName("U_Warehouse_ProductLike_ProductID_CreationUser");
        }
    }
}
