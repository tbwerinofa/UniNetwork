using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class QueuedEmailViewModel : BaseViewModel
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
        public int SentTries { get; set; }
        public DateTime? DontSendBeforeDate { get; set; }
        public int AttachedDownloadId { get; set; }
        public int EmailAccountId { get; set; }
        public DateTime? SentOn { get; set; }
        public int CurrentUserID { get; set; }

        public string SentOnString { get; set; }

        public bool IsRequeue { get; set; }



    }
}
