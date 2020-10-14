using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(OrderDetail), Schema = SchemaName.Store)]
    public partial class OrderDetail : AuditBase
    {

        public OrderDetail()
        {
        }

        public int QuoteId { get; set; }
        public int ProductSizeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

       public virtual Quote Quote { get; set; }
        public virtual ProductSize ProductSize { get; set; }
    }
}
