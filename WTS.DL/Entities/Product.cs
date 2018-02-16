namespace WTS.DL.Entities
{
    public class Product : IPkidEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}