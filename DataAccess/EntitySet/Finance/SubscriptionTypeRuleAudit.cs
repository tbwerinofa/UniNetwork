using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(SubscriptionTypeRuleAudit), Schema = SchemaName.Finance)]
    public partial class SubscriptionTypeRuleAudit : AuditBase
    {

        public SubscriptionTypeRuleAudit()
        {
            this.QuoteDetails = new HashSet<QuoteDetail>();
        }

        public int SubscriptionTypeRuleId { get; set; }

        public int? AgeGroupId { get; set; }

        public decimal AmountRand { get; set; }
        public Nullable<int> ActiveMonths { get; set; }

        public bool HasQuantity { get; set; }
        public bool HasRelations { get; set; }
        public virtual SubscriptionTypeRule SubscriptionTypeRule { get; set; }
        public virtual AgeGroup AgeGroup { get; set; }
        public virtual ICollection<QuoteDetail> QuoteDetails { get; set; }
    }
}
