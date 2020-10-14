using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Trophy), Schema = SchemaName.Accolade)]
    public class Trophy : AuditBase
    {

        public Trophy()
        {
            this.AwardTrophies = new HashSet<AwardTrophy>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int? DocumentId { get; set; }

        public virtual Document Document { get; set; }
 
        public virtual ICollection<AwardTrophy> AwardTrophies { get; set; }

    }
}
