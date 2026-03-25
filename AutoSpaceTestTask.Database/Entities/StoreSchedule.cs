namespace AutoSpaceTestTask.Database.Entities
{
    public class StoreSchedule : BaseEntity
    {
        public long StoreId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
        public bool IsDayOff { get; set; }

        public Store Store { get; set; } = default!;
    }
}