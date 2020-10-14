using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(ProductImage), Schema = SchemaName.Store)]
    public partial class ProductImage : AuditBase
    {
        public ProductImage()
        {
            this.FeaturedImages = new HashSet<FeaturedImage>();
        }
        public int DocumentId { get; set; }
        public int ProductId { get; set; }
        public bool IsFeatured { get; set; }
        public int Ordinal { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ICollection<FeaturedImage> FeaturedImages { get; set; }
        public virtual Document Document { get; set; }
    }
}
