using DataAccess.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RaceResult), Schema = SchemaName.Activity)]
    public partial class RaceResult : AuditBase
    {
        public int RaceDistanceId { get; set; }

        public int AgeGroupId { get; set; }
        public int MemberId { get; set; }
        public TimeSpan? TimeTaken { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public TimeSpan? AveragePace { get; set; }

        public int? Position { get; set; }

        public virtual Member Member { get; set; }

        public virtual AgeGroup AgeGroup { get; set; }
        public virtual RaceDistance RaceDistance { get; set; }
    }
}