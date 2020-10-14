    using DataAccess.Helpers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(Distance), Schema = SchemaName.Activity)]
    public partial class Distance : AuditBase
    {

        public Distance()
        {
            this.RaceDistances = new HashSet<RaceDistance>();
            this.TimeTrialDistances = new HashSet<TimeTrialDistance>();
            this.TrainingPlanDistances = new HashSet<TrainingPlanDistance>();
        }
        public string Name { get; set; }
        public int Measurement { get; set; }
        public int MeasurementUnitId { get; set; }
        public string Discriminator { get; set; }

        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public virtual ICollection<RaceDistance> RaceDistances { get; set; }

        public virtual ICollection<TimeTrialDistance> TimeTrialDistances { get; set; }
        public virtual ICollection<TrainingPlanDistance> TrainingPlanDistances { get; set; }
    }
}
