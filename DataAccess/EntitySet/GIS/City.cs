using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(City), Schema = SchemaName.Gis)]
    public class City:AuditBase
    {
        public City()
        {
            this.Towns = new HashSet<Town>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public int ProvinceId { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}