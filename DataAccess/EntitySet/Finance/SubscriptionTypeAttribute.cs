using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(SubscriptionTypeAttribute), Schema = SchemaName.Finance)]
    public partial class SubscriptionTypeAttribute : AuditBase
    {

        public int SubscriptionTypeId { get; set; }
        public string Name { get; set; }
 
        public virtual SubscriptionType SubscriptionType { get; set; }
    }
}
