using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(AgeGroup), Schema = SchemaName.Activity)]
    public partial class AgeGroup:AuditBase
    {
        public AgeGroup()
        {
            this.People = new HashSet<Person>();
            this.RaceResults = new HashSet<RaceResult>();
            this.SubscriptionTypeRules = new HashSet<SubscriptionTypeRule>();
            this.SubscriptionTypeRuleAudits = new HashSet<SubscriptionTypeRuleAudit>();
        }

        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<RaceResult> RaceResults { get; set; }

        public virtual ICollection<SubscriptionTypeRule> SubscriptionTypeRules { get; set; }
        public virtual ICollection<SubscriptionTypeRuleAudit> SubscriptionTypeRuleAudits { get; set; }
    }
}
