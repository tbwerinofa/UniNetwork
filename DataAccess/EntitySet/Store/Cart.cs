using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Cart), Schema = SchemaName.Store)]
    public partial class Cart:AuditBase
    {
        public string RecordId { get; set; }
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
        public int Count { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductSize ProductSize { get; set; }
    }
}
