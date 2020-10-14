using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(SubscriptionTypeRule), Schema = SchemaName.Finance)]
    public partial class SubscriptionTypeRule : AuditBase
    {
        public SubscriptionTypeRule()
        {
            this.SubscriptionTypeRuleAudits = new HashSet<SubscriptionTypeRuleAudit>();
        }

        public int SubscriptionTypeId { get; set; }

        public int? AgeGroupId { get; set; }

        public decimal AmountRand { get; set; }
        public Nullable<int> ActiveMonths { get; set; }

        public bool HasQuantity { get; set; }

        public bool HasRelations { get; set; }

        public virtual SubscriptionType SubscriptionType { get; set; }

        public virtual AgeGroup AgeGroup { get; set; }

        public virtual ICollection<SubscriptionTypeRuleAudit> SubscriptionTypeRuleAudits { get; set; }
    }
}
