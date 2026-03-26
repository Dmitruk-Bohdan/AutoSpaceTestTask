namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class StoreDetailsForUpdateResponseDto : StoreDetailsResponseDto
    {
        public List<long> StoreProductIds { get; init; } = default!;
    }
}
