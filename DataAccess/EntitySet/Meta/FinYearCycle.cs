
using DataAccess.Helpers;
using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(FinYearCycle), Schema = SchemaName.Meta)]
    public partial class FinYearCycle:AuditBase
    {
        public FinYearCycle()
        {
        }
    
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int FinYearId { get; set; }
        public int CycleId { get; set; }
        public virtual Cycle Cycle { get; set; }
        public virtual FinYear FinYear { get; set; }
    }
}
