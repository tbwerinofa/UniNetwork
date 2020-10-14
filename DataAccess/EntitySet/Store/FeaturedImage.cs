using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(FeaturedImage), Schema = SchemaName.Store)]
    public partial class FeaturedImage : AuditBase
    {
        public int ProductImageId { get; set; }
        public int FeaturedCategoryId { get; set; }
        public virtual ProductImage ProductImage { get; set; }
        public virtual FeaturedCategory FeaturedCategory { get; set; }
    }
}
