using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Helpers;

namespace DataAccess
{
    [Table(nameof(MemberStaging), Schema = SchemaName.Worker)]
     public class MemberStaging : AuditBase
    {
        public MemberStaging()
        {
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Initials { get; set; }
        public string OtherName { get; set; }
        public string IDNumber { get; set; }

        public string ContactNo { get; set; }
        public string Email { get; set; }

        public string EmmergencyContact1 { get; set; }
        public string EmmergencyContactNo1 { get; set; }
        public string EmmergencyContact2 { get; set; }
        public string EmmergencyContactNo2 { get; set; }

        public string Occupation { get; set; }
        public string Company { get; set; }

        public string WorkTelephone { get; set; }

        public string HomeTelephone { get; set; }

        public string PrevYearLicence { get; set; }
        public string CurrentYearLicence { get; set; }
        public string MedicalAidName { get; set; }
        public string MedicalAidNumber { get; set; }

        public System.DateTime BirthDate { get; set; }

        public int? TitleId { get; set; }
        public int GenderId { get; set; }

        public int IDTypeId { get; set; }

        public int CountryId { get; set; }

        public int AddressId { get; set; }

        public bool IsFromFront { get; set; }

        public bool IsFinalised { get; set; }

        public string Comment { get; set; }

        public bool IsRejected { get; set; }
        //public string FullName
        //{
        //    get { return FirstName + " " + Surname; }
        //}
        public virtual Gender Gender { get; set; }
        public virtual Title Title { get; set; }

        public virtual Country Country { get; set; }
        public virtual IDType IDType { get; set; }

        public virtual Address Address { get; set; }


    }
}
