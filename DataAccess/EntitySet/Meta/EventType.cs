using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(EventType), Schema = SchemaName.Meta)]
    public class EventType:AuditBase
    {
        public EventType()
        {
            this.Events = new HashSet<Event>();
        }


        public string Name { get; set; }

        public string Discriminator { get; set; }

        public virtual ICollection<Event> Events { get; set; }

    }
}