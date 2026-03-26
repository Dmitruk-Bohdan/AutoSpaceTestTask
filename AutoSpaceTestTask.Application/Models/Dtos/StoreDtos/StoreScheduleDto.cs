namespace AutoSpaceTestTask.Application.Models.Dtos.StoreDtos
{
    public class StoreScheduleDto
    {
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsDayOff { get; set; }
    }
}
