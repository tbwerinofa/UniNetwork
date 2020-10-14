using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TimeTrialDistance), Schema = SchemaName.Activity)]
    public partial class TimeTrialDistance : AuditBase
    {

        public TimeTrialDistance()
        {
            this.TimeTrialResults = new HashSet<TimeTrialResult>();
        }
        public int TimeTrialId { get; set; }
        public int DistanceId { get; set; }
        public virtual TimeTrial TimeTrial { get; set; }
        public virtual Distance Distance { get; set; }

        public virtual ICollection<TimeTrialResult> TimeTrialResults { get; set; }

    }
}
