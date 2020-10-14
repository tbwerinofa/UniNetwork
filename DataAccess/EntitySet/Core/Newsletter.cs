using System.Collections.Generic;

namespace DataAccess
{
    public class Newsletter : AuditBase
    {

        public Newsletter()
        {
        }

        public int IssueNo { get; set; }
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
 
    }
}
