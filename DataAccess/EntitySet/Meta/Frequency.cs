using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Frequency), Schema = SchemaName.Meta)]
    public class Frequency : AuditBase
    {
        public Frequency()
        {
            this.Events = new HashSet<Event>();
            this.Awards = new HashSet<Award>();
        }


        public string Name { get; set; }

        public string Discriminator { get; set; }

        public int? Recurrence { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

    }
}