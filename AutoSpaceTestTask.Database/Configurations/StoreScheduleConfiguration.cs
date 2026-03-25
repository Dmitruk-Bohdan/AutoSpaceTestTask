using AutoSpaceTestTask.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpaceTestTask.Database.Configurations
{
    public class StoreScheduleConfiguration : IEntityTypeConfiguration<StoreSchedule>
    {
        public void Configure(EntityTypeBuilder<StoreSchedule> builder)
        {
            builder.ToTable("StoreSchedules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DayOfWeek)
                .IsRequired();

            builder.Property(x => x.OpenTime)
                .IsRequired();

            builder.Property(x => x.CloseTime)
                .IsRequired();

            builder.Property(x => x.IsDayOff)
                .IsRequired();

            builder.HasOne(x => x.Store)
                .WithMany(x => x.Schedules)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.StoreId, x.DayOfWeek });
        }
    }
}