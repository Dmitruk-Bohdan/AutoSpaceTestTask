namespace AutoSpaceTestTask.Application.Models.Dtos.StoreDtos
{
    public class StoreDetailsForUpdateResponseDto : StoreDetailsResponseDto
    {
        public List<long> StoreProductIds { get; init; } = default!;
    }
}
