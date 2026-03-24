namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class UpdateStoreDto
    {
        public long StoreId { get; init; }
        public string Name { get; init; } = default!;
        public string Address { get; init; } = default!;
        public List<StoreScheduleDto> StoreSchedulesDto { get; init; } = default!;
        public string TimeZone { get; init; } = default!;
    }

    public class StoreScheduleDto
    {
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
