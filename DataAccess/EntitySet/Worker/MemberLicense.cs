using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Helpers;

namespace DataAccess
{
    [Table(nameof(MemberLicense), Schema = SchemaName.Worker)]
    public class MemberLicense : AuditBase
    {
        public MemberLicense()
        {
        }

        public int FinYearId { get; set; }
        public int MemberId { get; set; }

        public string LicenseNo { get; set; }

        public virtual FinYear FinYear { get; set; }
        public virtual Member Member { get; set; }

    }
}
