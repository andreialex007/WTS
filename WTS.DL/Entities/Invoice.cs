using System;
using System.Collections.Generic;

namespace WTS.DL.Entities
{
    public class Invoice : IPkidEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int Type { get; set; }
        public string Customer { get; set; }

        public int? FromWarehouseId { get; set; }
        public Warehouse FromWarehouse { get; set; }
        public int? ToWarehouseId { get; set; }
        public Warehouse ToWarehouse { get; set; }

        public int? FromSupplierId { get; set; }
        public Supplier FromSupplier { get; set; }
        public int? ToSupplierId { get; set; }
        public Supplier ToSupplier { get; set; }

        public int? FromStoreId { get; set; }
        public Store FromStore { get; set; }
        public int? ToStoreId { get; set; }
        public Store ToStore { get; set; }



        public List<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    }
}