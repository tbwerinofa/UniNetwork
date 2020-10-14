using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Address : AuditBase
    {
        public Address()
        {
            this.MemberStagings = new HashSet<MemberStaging>();
        }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Code { get; set; }
        public int SuburbId { get; set; }


        public virtual Suburb Suburb { get; set; }

        public virtual ICollection<MemberStaging> MemberStagings { get; set; }
    }

}