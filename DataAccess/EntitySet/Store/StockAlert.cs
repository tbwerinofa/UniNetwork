using System;
using System.Collections.Generic;
namespace DataAccess
{
    public partial class StockAlert : AuditBase
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public Nullable<int> ProductSizeId { get; set; }
        public bool IsAlertSent { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ProductSize ProductSize { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
