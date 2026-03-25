namespace AutoSpaceTestTask.Database.Entities
{
    public class Store : BaseEntity
    {
        public Guid Code { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;

        public ICollection<StoreSchedule> Schedules { get; set; } = new List<StoreSchedule>();
        public ICollection<StoreProduct> StoreProducts { get; set; } = new List<StoreProduct>();
    }
}
