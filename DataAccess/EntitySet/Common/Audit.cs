using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuditBase
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.DateTime CreatedTimestamp { get; set; }
        public Nullable<System.DateTime> UpdatedTimestamp { get; set; }

        [Required]
        public string CreatedUserId { get; set; }

        public string UpdatedUserId { get; set; }
        public virtual ApplicationUser CreatedUser { get; set; }
        public virtual ApplicationUser UpdatedUser { get; set; }
    }
}
