using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{

    public partial class Person:AuditBase
    {

        public Person()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Members = new HashSet<Member>();
            this.RaceResultImports = new HashSet<RaceResultImport>();
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string ContactNo { get; set; }
        public string Email { get; set; }

        public string Initials { get; set; }
        public string OtherName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + Surname; }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int AgeGroupId { get; set; }
        public string IDNumber { get; set; }

        public System.DateTime BirthDate { get; set; }

        public int? TitleId { get; set; }
        public int GenderId { get; set; }
        public int IDTypeId { get; set; }

        public int? DocumentId { get; set; }

        public int CountryId { get; set; }

        public int AddressId { get; set; }

        public string PersonGuid { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Title Title { get; set; }

        public virtual Document Document { get; set; }

        public virtual Country Country { get; set; }

        public virtual AgeGroup AgeGroup { get; set; }
        public virtual IDType IDType { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<RaceResultImport> RaceResultImports { get; set; }

    }
}
