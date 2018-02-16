using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTS.DL.Entities
{
    public class Supplier : IPkidEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        [InverseProperty("FromSupplier")]
        public List<Invoice> Out { get; set; } = new List<Invoice>();
        [InverseProperty("ToSupplier")]
        public List<Invoice> In { get; set; } = new List<Invoice>();
    }
}