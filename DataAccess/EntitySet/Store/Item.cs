using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Item), Schema = SchemaName.Store)]
    public partial class Item : AuditBase
    {
        public int Quantity { get; set; }
        public int ProductSizeId { get; set; }
        public virtual ProductSize ProductSize { get; set; }

    }
}
