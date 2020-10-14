using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Town), Schema = SchemaName.Gis)]

    public partial class Town : AuditBase
    {
        public Town()
        {
            this.Suburbs = new HashSet<Suburb>();
        }

        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Suburb> Suburbs { get; set; }
    }
}
