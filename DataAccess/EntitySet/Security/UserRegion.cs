using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRegion:AuditBase
    {

        public UserRegion()
        {
        }
        public string ApplicationUserId { get; set; }
        public int RegionId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Province Region { get; set; }


    }
}
