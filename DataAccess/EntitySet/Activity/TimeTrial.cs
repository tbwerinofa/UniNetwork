using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TimeTrial), Schema = SchemaName.Activity)]
    public partial class TimeTrial:AuditBase
    {
        
        public TimeTrial()
        {
            this.TimeTrialDistances = new HashSet<TimeTrialDistance>();

        }
        public int CalendarId { get; set; }
        public int RaceTypeId { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual RaceType RaceType { get; set; }
        public virtual ICollection<TimeTrialDistance> TimeTrialDistances { get; set; }

    }
}
