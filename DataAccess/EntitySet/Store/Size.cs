using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Size), Schema = SchemaName.Store)]
    public partial class Size : AuditBase
    {

        public Size()
        {
            this.ProductSizes = new HashSet<ProductSize>();
            //this.OrderDetails = new HashSet<OrderDetail>();
        }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Ordinal { get; set; }

        public virtual ICollection<ProductSize> ProductSizes { get; set; }

       // public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
