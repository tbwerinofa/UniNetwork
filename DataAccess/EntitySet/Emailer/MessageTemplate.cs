using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(MessageTemplate), Schema = SchemaName.Emailer)]
    public partial class MessageTemplate:AuditBase
    {

        public MessageTemplate()
        {
            this.QuoteStatuses = new HashSet<QuoteStatus>();
            this.ApplicationRoleMessageTemplates = new HashSet<ApplicationRoleMessageTemplate>();
        }

        public string Name { get; set; }
        public string BccEmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<int> DelayBeforeSend { get; set; }
        public int EmailAccountId { get; set; }
        public bool LimitedToApplications { get; set; }
        public string FromAddress { get; set; }
        public int DelayHours { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }
        public virtual ICollection<QuoteStatus> QuoteStatuses { get; set; }
        public virtual ICollection<ApplicationRoleMessageTemplate> ApplicationRoleMessageTemplates { get; set; }
    }
}