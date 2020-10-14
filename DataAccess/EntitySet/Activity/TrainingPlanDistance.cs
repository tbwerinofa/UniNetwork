using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TrainingPlanDistance), Schema = SchemaName.Activity)]
    public partial class TrainingPlanDistance:AuditBase
    {

        public TrainingPlanDistance()
        {
        }
        public int TrainingPlanId { get; set; }
        public int DistanceId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
        public virtual Distance Distance { get; set; }

    }
}
