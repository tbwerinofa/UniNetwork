using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Helpers;

namespace DataAccess
{
    [Table(nameof(MemberPosition), Schema = SchemaName.Worker)]
    public class MemberPosition : AuditBase
    {

        public MemberPosition()
        {
            //this.Participants = new HashSet<Participant>();
        }

        public DateTime StartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        //public int? EmployeeId { get; set; }

        public int MemberId { get; set; }

        public int FinYearId { get; set; }

        public int OrganogramId { get; set; }

        public int PositionAuditId { get; set; }

        //public virtual PositionAudit PositionAudit { get; set; }
        //public virtual Employee Employee { get; set; }

        public virtual Member Member { get; set; }

        public virtual FinYear FinYear { get; set; }

       // public virtual Organogram Organogram { get; set; }

        //public virtual ICollection<Participant> Participants { get; set; }

    }
}
