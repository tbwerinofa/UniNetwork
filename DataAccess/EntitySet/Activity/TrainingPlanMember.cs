using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TrainingPlanMember), Schema = SchemaName.Activity)]
    public partial class TrainingPlanMember:AuditBase
    {

        public TrainingPlanMember()
        {
        }
        public int TrainingPlanId { get; set; }
        public int MemberId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
        public virtual Member Member { get; set; }

    }
}
