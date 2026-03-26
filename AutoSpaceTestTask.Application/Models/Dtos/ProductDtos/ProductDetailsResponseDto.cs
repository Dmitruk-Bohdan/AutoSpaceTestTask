namespace AutoSpaceTestTask.Application.Models.Dtos.ProductDtos
{
    public class ProductDetailsResponseDto
    {
        public Guid Code { get; set; } = default!;
        public string Article { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string GroupName { get; set; } = default!;
    }
}