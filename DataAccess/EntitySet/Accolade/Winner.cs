using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(Winner), Schema = SchemaName.Accolade)]
    public class Winner : AuditBase
    {

        public Winner()
        {
        }
        public int FinYearId { get; set; }
        public int CalendarMonthId { get; set; }
        public int MemberId { get; set; }
        public int AwardId { get; set; }
        public virtual FinYear FinYear { get; set; }
        public virtual CalendarMonth CalendarMonth { get; set; }
        public virtual Member Member { get; set; }
        public virtual Award Award { get; set; }

    }
}
