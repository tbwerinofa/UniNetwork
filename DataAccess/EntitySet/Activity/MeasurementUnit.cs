using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(MeasurementUnit), Schema = SchemaName.Activity)]
    public partial class MeasurementUnit:AuditBase
    {

        public MeasurementUnit()
        {
            this.Distances = new HashSet<Distance>();
        }

        public string Name { get; set; }

        public virtual ICollection<Distance> Distances { get; set; }

    }
}
