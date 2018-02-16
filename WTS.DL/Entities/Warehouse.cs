using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WTS.DL.Entities
{
    public class Warehouse : IPkidEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        [InverseProperty("FromWarehouse")]
        public List<Invoice> Out { get; set; } = new List<Invoice>();
        [InverseProperty("ToWarehouse")]
        public List<Invoice> In { get; set; } = new List<Invoice>();
    }
}