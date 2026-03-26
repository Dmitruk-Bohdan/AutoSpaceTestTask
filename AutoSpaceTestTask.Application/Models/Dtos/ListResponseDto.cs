namespace AutoSpaceTestTask.Application.Models.Dtos
{
    public class ListResponseDto<T>
    {
        public List<T> Items { get; set; } = new();
    }
}
