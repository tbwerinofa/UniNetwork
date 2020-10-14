using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess
{
  
    public class ApplicationRole : IdentityRole
    {

        public ApplicationRole()
        {
            this.ApplicationRoleMenus = new HashSet<ApplicationRoleMenu>();
            this.ApplicationRoleMessageTemplates = new HashSet<ApplicationRoleMessageTemplate>();
        }

        public string TenantId { get; set; }
        public virtual ICollection<ApplicationRoleMenu> ApplicationRoleMenus { get; set; }
        public virtual ICollection<ApplicationRoleMessageTemplate> ApplicationRoleMessageTemplates { get; set; }
    }
}
