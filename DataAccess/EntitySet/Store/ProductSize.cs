using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(ProductSize), Schema = SchemaName.Store)]
    public partial class ProductSize : AuditBase
    {
        public ProductSize()
        {
            this.Items = new HashSet<Item>();
            this.StockAlerts = new HashSet<StockAlert>();
            this.Carts = new HashSet<Cart>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int ProductId { get; set; }
        public int SizeId { get; set; } 
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<StockAlert> StockAlerts { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
