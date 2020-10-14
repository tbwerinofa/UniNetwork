using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(Race), Schema = SchemaName.Activity)]
    public partial class Race:AuditBase
    {
        
        public Race()
        {
            this.RaceDistances = new HashSet<RaceDistance>();
            this.RaceOrganisations = new HashSet<RaceOrganisation>();
        }
        public string Theme { get; set; }
        public int FinYearId { get; set; }
        public int RaceDefinitionId { get; set; }

        public virtual RaceDefinition RaceDefinition { get; set; }

        public virtual FinYear FinYear { get; set; }
        public virtual ICollection<RaceDistance> RaceDistances { get; set; }
        public virtual ICollection<RaceOrganisation> RaceOrganisations { get; set; }
    }
}
