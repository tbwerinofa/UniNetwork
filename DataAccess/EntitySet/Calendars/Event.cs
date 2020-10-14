using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Event), Schema = SchemaName.Calendar)]
    public class Event:AuditBase
    {
        public Event()
        {
            this.Calendars = new HashSet<Calendar>();
            this.TrainingPlans = new HashSet<TrainingPlan>();
        }


        public string Name { get; set; }

        public bool RequiresRsvp { get; set; }
        public bool RequiresSubscription { get; set; }
        public int FrequencyId { get; set; }
        public int EventTypeId { get; set; }
        public virtual Frequency Frequency { get; set; }
        public virtual EventType EventType { get; set; }
        public virtual ICollection<Calendar> Calendars { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }

    }
}