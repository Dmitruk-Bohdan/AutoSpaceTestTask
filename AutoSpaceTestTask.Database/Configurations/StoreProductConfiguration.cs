using AutoSpaceTestTask.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpaceTestTask.Database.Configurations
{
    public class StoreProductConfiguration : IEntityTypeConfiguration<StoreProduct>
    {
        public void Configure(EntityTypeBuilder<StoreProduct> builder)
        {
            builder.ToTable("StoreProducts");

            builder.HasKey(x => new { x.StoreId, x.ProductId });

            builder.Property(x => x.StoreId)
                .IsRequired();

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.HasOne(x => x.Store)
                .WithMany(s => s.StoreProducts)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany(p => p.StoreProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}