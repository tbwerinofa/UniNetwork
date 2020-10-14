using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(ProductCategory), Schema = SchemaName.Store)]
    public partial class ProductCategory : AuditBase
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Ordinal { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
