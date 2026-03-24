namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class ProductListResponseDto
    {
        public List<ProductDetailsResponseDto> Items { get; set; } = new();
    }
}
