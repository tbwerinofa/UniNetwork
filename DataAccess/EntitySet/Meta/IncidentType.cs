using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(IncidentType), Schema = SchemaName.Meta)]
    public class IncidentType:AuditBase
    {
        public IncidentType()
        {
        }

        public string Name { get; set; }

        public int IncidentGroupId { get; set; }
 
        public virtual IncidentGroup IncidentGroup { get; set; }


    }
}