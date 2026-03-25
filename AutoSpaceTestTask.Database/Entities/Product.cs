namespace AutoSpaceTestTask.Database.Entities
{
    public class Product : BaseEntity
    {
        public Guid Code { get; set; }       
        public string Article { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Name { get; set; } = default!;

        public long GroupId { get; set; }
        public ProductGroup Group { get; set; } = default!;

        public ICollection<StoreProduct> StoreProducts { get; set; } = new List<StoreProduct>();
    }
}