using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TrainingPlan), Schema = SchemaName.Activity)]
    public partial class TrainingPlan:AuditBase
    {
        
        public TrainingPlan()
        {
            this.TrainingPlanDistances = new HashSet<TrainingPlanDistance>();
            this.TrainingPlanMembers = new HashSet<TrainingPlanMember>();
        }
        public string Name { get; set; }
        public string Objective { get; set; }
        public int FinYearId { get; set; }
        public int EventId { get; set; }

        public virtual FinYear FinYear { get; set; }

        public virtual Event Event { get; set; }
        public virtual ICollection<TrainingPlanDistance> TrainingPlanDistances { get; set; }
        public virtual ICollection<TrainingPlanMember> TrainingPlanMembers { get; set; }
        public virtual ICollection<TrainingPlanRaceDefinition> TrainingPlanRaceDefinitions { get; set; }


    }
}
