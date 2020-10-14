using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Calendar), Schema = SchemaName.Calendar)]
    public class Calendar:AuditBase
    {
        public Calendar()
        {
            this.TimeTrials = new HashSet<TimeTrial>();
            this.Moderators = new HashSet<Moderator>();
        }

        public DateTime ScheduleDate { get; set; }

        public DateTime? RevisedDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public int FinYearId { get; set; }
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public virtual Event Event { get; set; }
        public virtual FinYear FinYear { get; set; }
        public virtual Venue Venue { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<TimeTrial> TimeTrials { get; set; }
        public virtual ICollection<Moderator> Moderators { get; set; }


    }
}