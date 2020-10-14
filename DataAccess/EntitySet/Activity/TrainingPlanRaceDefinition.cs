using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TrainingPlanRaceDefinition), Schema = SchemaName.Activity)]
    public partial class TrainingPlanRaceDefinition:AuditBase
    {

        public TrainingPlanRaceDefinition()
        {
        }
        public int TrainingPlanId { get; set; }
        public int RaceDefinitionId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
        public virtual RaceDefinition RaceDefinition { get; set; }

    }
}
