using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(Award), Schema = SchemaName.Accolade)]
    public partial class Award : AuditBase
    {
        public Award()
        {
            this.AwardTrophies = new HashSet<AwardTrophy>();
            this.Winners = new HashSet<Winner>();
        }

        public string Name { get; set; }
        public int FrequencyId { get; set; }

        public int Ordinal { get; set; }

        public int? GenderId { get; set; }
        public bool HasTrophy { get; set; }

        public virtual Frequency Frequency { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual ICollection<AwardTrophy> AwardTrophies { get; set; }
        public virtual ICollection<Winner> Winners { get; set; }
    }

}