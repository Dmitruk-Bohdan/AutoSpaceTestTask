namespace AutoSpaceTestTask.Application.Models.Dtos.ProductDtos
{
    public class ProductPreviewResponseDto
    {
        public long ProductId { get; set; }
        public string Article { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
