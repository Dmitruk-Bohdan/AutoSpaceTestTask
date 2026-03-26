namespace AutoSpaceTestTask.Application.Models.Dtos.StoreDtos
{
    public class StoreDetailsResponseDto
    {
        public long StoreId { get; set; }
        public Guid Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int ProductsCount { get; set; }
        public List<StoreScheduleDto> StoreSchedulesDto { get; init; } = default!;
    }
}
