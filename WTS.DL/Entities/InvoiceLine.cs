namespace WTS.DL.Entities
{
    public class InvoiceLine : IPkidEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Qty { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}