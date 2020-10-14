using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{

    [Table(nameof(EmailAccount), Schema = SchemaName.Emailer)]
    public partial class EmailAccount:AuditBase
    {
        public EmailAccount()
        {
            this.MessageTemplates = new HashSet<MessageTemplate>();
            this.QueuedEmails = new HashSet<QueuedEmail>();
        }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }

        public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }
        
        public virtual ICollection<QueuedEmail> QueuedEmails { get; set; }
    }
}