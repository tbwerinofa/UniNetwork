using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Venue), Schema = SchemaName.Meta)]
    public partial class Venue : AuditBase
    {
        public Venue()
        {
            this.Calendars = new HashSet<Calendar>();
        }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
    }

}