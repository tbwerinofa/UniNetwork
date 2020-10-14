using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Helpers;

namespace DataAccess
{
    [Table(nameof(MemberMapping), Schema = SchemaName.Worker)]
    public class MemberMapping : AuditBase
    {
        public MemberMapping()
        {
        }


        public int MemberId { get; set; }

        public int RelationMemberId { get; set; }

        public virtual Member Member { get; set; }

        public virtual Member RelationMember { get; set; }


    }
}
