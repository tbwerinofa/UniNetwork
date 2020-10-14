using DataAccess.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(QueuedEmail), Schema = SchemaName.Emailer)]
    public partial class QueuedEmail:AuditBase
    {

        public int Priority { get; set; }
        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string ReplyTo { get; set; }
        public string ReplyToName { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFilePath { get; set; }
        public string AttachmentFileName { get; set; }
        public Nullable<System.DateTime> DontSendBeforeDate { get; set; }
        public int SentTries { get; set; }
        public Nullable<System.DateTime> SentOn { get; set; }
        public int EmailAccountId { get; set; }

        public byte[] FileByte { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }
    }
}
