namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class StorePreviewResponseDto
    {
        public long StoreId { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public StoreScheduleDto TodaySchedule { get; set; } = default!;
    }
}
