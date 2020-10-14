using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(FeaturedCategory), Schema = SchemaName.Store)]
    public partial class FeaturedCategory : AuditBase
    {
        public FeaturedCategory()
        {
            this.FeaturedImages = new HashSet<FeaturedImage>();
        }
    
       public string Name { get; set; }
        public int Ordinal { get; set; }

        public virtual ICollection<FeaturedImage> FeaturedImages { get; set; }

    }
}
