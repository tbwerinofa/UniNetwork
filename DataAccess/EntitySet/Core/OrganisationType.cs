using System.Collections.Generic;

namespace DataAccess
{
    public class OrganisationType : AuditBase
    {
        public OrganisationType()
        {
            this.Organisations = new HashSet<Organisation>();
        }

       
        public string Name { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; }
    }
}