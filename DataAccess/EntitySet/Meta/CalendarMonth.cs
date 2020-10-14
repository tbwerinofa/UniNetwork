using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(CalendarMonth), Schema = SchemaName.Meta)]
    public partial class CalendarMonth : AuditBase
    {
        public CalendarMonth()
        {
            this.Winners = new HashSet<Winner>();
        }
        public string Name { get; set; }
        public int Ordinal  { get; set; }
        public virtual ICollection<Winner> Winners { get; set; }
    }

}