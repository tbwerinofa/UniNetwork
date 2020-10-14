using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RaceType), Schema = SchemaName.Activity)]
    public partial class RaceType:AuditBase
    {
        
        public RaceType()
        {
            this.RaceDefinitions = new HashSet<RaceDefinition>();
            this.TimeTrials = new HashSet<TimeTrial>();
        }

        public string Name { get; set; }
        public virtual ICollection<RaceDefinition> RaceDefinitions { get; set; }
        public virtual ICollection<TimeTrial> TimeTrials { get; set; }
    }
}
