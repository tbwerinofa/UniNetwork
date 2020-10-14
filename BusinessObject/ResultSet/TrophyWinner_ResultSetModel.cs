using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject.ResultSet
{
    public partial class TrophyWinner_ResultSetModel
    {
        public int TrophyId { get; set; }
        public string Trophy { get; set; }
        public string Description { get; set; }
        public int? DocumentId { get; set; }
        public string Award { get; set; }
        public string Gender { get; set; }
        public string MemberNo { get; set; }
        public string FullName { get; set; }
        public int? DocumentTypeId { get; set; }
        public byte[] DocumentData { get; set; }
        public string DocumentNameGuid { get; set; }
        public string DocumentName { get; set; }
        public int FinYear { get; set; }
        public int Ordinal { get; set; }
    }
}
