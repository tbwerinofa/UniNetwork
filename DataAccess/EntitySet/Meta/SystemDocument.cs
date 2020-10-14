using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{

    [Table(nameof(SystemDocument), Schema = SchemaName.Meta)]
    public partial class SystemDocument : AuditBase
    {
        public int FinYearId { get; set; }

        public int DocumentId { get; set; }

        public int Ordinal { get; set; }

        public virtual Document Document { get; set; }

        public virtual FinYear FinYear { get; set; }
    }
}
