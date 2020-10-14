using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Article : AuditBase
    {

        public Article()
        {
            this.Newsletters = new HashSet<Newsletter>();
        }

        public string Name { get; set; }
        public string Author { get; set; }

        public DateTime PublishDate { get; set; }
        public string Body { get; set; }
        public int FinYearId { get; set; }
        public int CalendarMonthId { get; set; }

        public virtual FinYear FinYear { get; set; }

        public virtual CalendarMonth CalendarMonth { get; set; }
 
        public virtual ICollection<Newsletter> Newsletters { get; set; }

    }
}
