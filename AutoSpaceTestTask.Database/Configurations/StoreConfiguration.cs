using AutoSpaceTestTask.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(x => x.Schedules)
            .WithOne(x => x.Store)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Products)
            .WithMany(x => x.Stores)
            .UsingEntity<Dictionary<string, object>>(
                "StoreProducts",
                j => j.HasOne<Product>()
                      .WithMany()
                      .HasForeignKey("ProductId"),
                j => j.HasOne<Store>()
                      .WithMany()
                      .HasForeignKey("StoreId"),
                j =>
                {
                    j.HasKey("StoreId", "ProductId");
                    j.ToTable("StoreProducts");
                });

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}