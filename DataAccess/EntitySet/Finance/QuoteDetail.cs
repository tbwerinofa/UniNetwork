using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(QuoteDetail), Schema = SchemaName.Finance)]
    public partial class QuoteDetail:AuditBase
    {
        public QuoteDetail()
        {
            this.Subscriptions = new HashSet<Subscription>();
            this.SubscriptionHistories = new HashSet<SubscriptionHistory>();
        }

        public int QuoteId { get; set; }
        public int SubscriptionTypeRuleAuditId { get; set; }
        public int Quantity { get; set; }
        public int ItemNo { get; set; }
 
        public virtual Quote Quote { get; set; }
        public virtual SubscriptionTypeRuleAudit SubscriptionTypeRuleAudit { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; }
    }
}
