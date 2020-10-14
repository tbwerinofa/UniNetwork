using DataAccess.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(TimeTrialResult), Schema = SchemaName.Activity)]
    public partial class TimeTrialResult : AuditBase
    {
        public int TimeTrialDistanceId { get; set; }

        public int MemberId { get; set; }
        public TimeSpan? TimeTaken { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public TimeSpan? AveragePace { get; set; }

        public int? Position { get; set; }

        public virtual Member Member { get; set; }

        public virtual TimeTrialDistance TimeTrialDistance { get; set; }

    }
}