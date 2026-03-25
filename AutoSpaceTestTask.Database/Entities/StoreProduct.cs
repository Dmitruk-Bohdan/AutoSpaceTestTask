namespace AutoSpaceTestTask.Database.Entities
{
    public class StoreProduct
    {
        public long StoreId { get; set; }
        public Store Store { get; set; } = default!;

        public long ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}