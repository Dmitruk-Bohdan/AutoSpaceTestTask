namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class StoreDetailsResponseDto
    {
        public long StoreId { get; set; }
        public Guid Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int ProductsCount { get; set; } 
        public List<string> Schedules { get; set; } = new();
    }
}
