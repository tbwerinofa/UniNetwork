using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(IncidentGroup), Schema = SchemaName.Meta)]
    public partial class IncidentGroup : AuditBase
    {
        public IncidentGroup()
        {
            this.IncidentTypes = new HashSet<IncidentType>();
        }
        public string Name { get; set; }
        public virtual ICollection<IncidentType> IncidentTypes { get; set; }
    }

}