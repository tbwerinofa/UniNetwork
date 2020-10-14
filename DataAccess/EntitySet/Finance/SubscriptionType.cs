using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(SubscriptionType), Schema = SchemaName.Finance)]
    public partial class SubscriptionType : AuditBase
    {

        public SubscriptionType()
        {
            this.SubscriptionTypeRules = new HashSet<SubscriptionTypeRule>();
            this.SubscriptionTypeAttributes = new HashSet<SubscriptionTypeAttribute>();
        }
 
        public string Name { get; set; }

        public string Discriminator { get; set; }

        public virtual ICollection<SubscriptionTypeRule> SubscriptionTypeRules { get; set; }

        public virtual ICollection<SubscriptionTypeAttribute> SubscriptionTypeAttributes { get; set; }
    }
}
