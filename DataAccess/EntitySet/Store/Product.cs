using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Product), Schema = SchemaName.Store)]
    public partial class Product : AuditBase
    {

        public Product()
        {
            this.ProductSizes = new HashSet<ProductSize>();
            this.ProductImages = new HashSet<ProductImage>();
            this.StockAlerts = new HashSet<StockAlert>();
            this.Carts = new HashSet<Cart>();
           // this.OrderDetails = new HashSet<OrderDetail>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductCategoryId { get; set; }
        public int Ordinal { get; set; }
        public decimal Price { get; set; }

        public bool IsMain { get; set; }
        public int Hits { get; set; }
        public int Sold { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<decimal> Vat { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<ProductSize> ProductSizes { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<StockAlert> StockAlerts { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

       // public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
