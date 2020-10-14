using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationRoleMenu : AuditBase
    {

        public ApplicationRoleMenu()
        {
        }
        public string ApplicationRoleId { get; set; }
        public int MenuId { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; }
        public virtual Menu Menu { get; set; }

    }
}
