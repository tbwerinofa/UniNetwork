using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Quote), Schema = SchemaName.Finance)]
    public partial class Quote:AuditBase
    {

        public Quote()
        {
            this.QuoteDetails = new HashSet<QuoteDetail>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.PayFastNotifies = new HashSet<PayFastNotify>();
        }
    

        public string QuoteNo { get; set; }
        public int QuoteStatusId { get; set; }
        public string QuoteUserId { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string PaymentReference { get; set; }

        public int FinYearId { get; set; }
 
        public virtual ApplicationUser QuoteUser { get; set; }
        public virtual QuoteStatus QuoteStatus { get; set; }
    
        public virtual ICollection<QuoteDetail> QuoteDetails { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<PayFastNotify> PayFastNotifies { get; set; }
        public virtual FinYear FinYear { get; set; }
    }
}
