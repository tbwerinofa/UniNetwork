using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Organisation : AuditBase
    {

        public Organisation()
        {
            this.RaceOrganisations = new HashSet<RaceOrganisation>();
        }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int ProvinceId { get; set; }
        public int OrganisationTypeId { get; set; }

        public virtual OrganisationType OrganisationType { get; set; }

        public virtual Province Province { get; set; }
 
        public virtual ICollection<RaceOrganisation> RaceOrganisations { get; set; }

    }
}
