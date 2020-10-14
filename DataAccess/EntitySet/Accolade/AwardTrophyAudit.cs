using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(AwardTrophyAudit), Schema = SchemaName.Accolade)]
    public class AwardTrophyAudit : AuditBase
    {
        public AwardTrophyAudit()
        {
           
        }
        public int FinYearId { get; set; }
        public int AwardTrophyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public AwardTrophy AwardTrophy { get; set; }
        public FinYear FinYear { get; set; }

     
    }
}