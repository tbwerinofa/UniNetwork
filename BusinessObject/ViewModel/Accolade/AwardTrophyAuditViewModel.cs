using System;

namespace BusinessObject
{
    public class AwardTrophyAuditViewModel : BaseViewModel
    {

        public int AwardTrophyAuditId { get; set; }
        public int FinYearId { get; set; }
        public int AwardTrophyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Award { get; set; }
        public int FinYear { get; set; }
        public string Trophy { get; set; }

    }
}
