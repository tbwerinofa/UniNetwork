using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(GlobalRegion), Schema = SchemaName.Gis)]
    public class GlobalRegion : AuditBase
    {
        public GlobalRegion()
        {
            this.Countries = new HashSet<Country>();
        }

        public string Name { get; set; }

        public virtual ICollection<Country> Countries { get; set; }


    }
}