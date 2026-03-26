namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class ProductPreviewResponseDto
    {
        public long ProductId { get; set; }
        public string Article { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
