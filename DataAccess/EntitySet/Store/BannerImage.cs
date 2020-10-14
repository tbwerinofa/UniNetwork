using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{

    [Table(nameof(BannerImage), Schema = SchemaName.Store)]
    public partial class BannerImage : AuditBase
    {
        public int Ordinal { get; set; }

        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }
    }
}
