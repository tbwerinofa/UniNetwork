using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(AwardTrophy), Schema = SchemaName.Accolade)]
    public class AwardTrophy : AuditBase
    {

        public AwardTrophy()
        {
             this.AwardTrophyAudits = new HashSet<AwardTrophyAudit>();
        }

        public int AwardId { get; set; }
        public int FinYearId { get; set; }
        public int TrophyId { get; set; }
        public DateTime StartDate { get; set; }

        public virtual FinYear FinYear { get; set; }
        public virtual Award Award { get; set; }

        public virtual Trophy Trophy { get; set; }
 
       public virtual ICollection<AwardTrophyAudit> AwardTrophyAudits { get; set; }

    }
}
