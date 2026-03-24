namespace AutoSpaceTestTask.Database.Entities
{
    public class ProductGroup : BaseEntity
    {
        public string Name { get; set; } = default!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}