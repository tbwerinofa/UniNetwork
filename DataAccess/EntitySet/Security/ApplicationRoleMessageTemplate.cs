using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationRoleMessageTemplate : AuditBase
    {

        public ApplicationRoleMessageTemplate()
        {

        }
        public string ApplicationRoleId { get; set; }
        public int MessageTemplateId { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }
        public virtual MessageTemplate MessageTemplate { get; set; }


    }
}
