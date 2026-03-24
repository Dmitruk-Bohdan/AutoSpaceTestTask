namespace AutoSpaceTestTask.Database.Entities
{
    public class StoreSchedule : BaseEntity
    {
        public long StoreId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan UtcStartTime { get; set; }
        public TimeSpan UtcEndTime { get; set; }

        public Store Store { get; set; } = default!;
    }
}