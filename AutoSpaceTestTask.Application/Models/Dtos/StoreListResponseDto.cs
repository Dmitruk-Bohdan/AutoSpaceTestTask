namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class StoreListResponseDto
    {
        public List<StoreDetailsResponseDto> Items { get; set; } = new();
    }
}
