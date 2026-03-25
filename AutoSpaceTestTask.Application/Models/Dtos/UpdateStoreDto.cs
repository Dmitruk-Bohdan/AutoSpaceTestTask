namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class UpdateStoreDto
    {
        public long StoreId { get; init; }
        public string Name { get; init; } = default!;
        public string Address { get; init; } = default!;
        public List<StoreScheduleDto> StoreSchedulesDto { get; init; } = new();
        public List<long> StoreProductIds { get; init; } = new();
    }
}
