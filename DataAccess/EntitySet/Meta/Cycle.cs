using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

    
namespace DataAccess
{
    [Table(nameof(Cycle), Schema = SchemaName.Meta)]
    public partial class Cycle:AuditBase
    {
        public Cycle()
        {
            this.FinYearCycles = new HashSet<FinYearCycle>();
        }
    
        public int Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FinYearCycle> FinYearCycles { get; set; }
    }
}
