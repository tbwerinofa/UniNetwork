using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Suburb), Schema = SchemaName.Gis)]

    public partial class Suburb : AuditBase
    {
        public Suburb()
        {
            this.Addresses = new HashSet<Address>();
        }
        public string Name { get; set; }
        public string BoxCode { get; set; }
        public string StreetCode { get; set; }
        public string PostCode { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public int TownId { get; set; }
    
        public virtual Town Town { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
