using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{

    [Table(nameof(Subscription), Schema = SchemaName.Finance)]
    public partial class Subscription : AuditBase
    {
        public Subscription()
        {
            this.SubscriptionHistories = new HashSet<SubscriptionHistory>();
        }

        public int MemberId { get; set; }

        public int QuoteDetailId { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }

        public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; }
        public virtual QuoteDetail QuoteDetail { get; set; }

        public virtual Member Member { get; set; }
    }
}
