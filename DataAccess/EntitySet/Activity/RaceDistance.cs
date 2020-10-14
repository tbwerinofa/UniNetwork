using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RaceDistance), Schema = SchemaName.Activity)]
    public partial class RaceDistance:AuditBase
    {

        public RaceDistance()
        {
            this.RaceResults = new HashSet<RaceResult>();
            this.RaceResultImports = new HashSet<RaceResultImport>();
        }
        public int RaceId { get; set; }
        public int DistanceId { get; set; }
        public DateTime EventDate { get; set; }
        public virtual Race Race { get; set; }
        public virtual Distance Distance { get; set; }
        public virtual ICollection<RaceResult> RaceResults { get; set; }
        public virtual ICollection<RaceResultImport> RaceResultImports { get; set; }
    }
}
