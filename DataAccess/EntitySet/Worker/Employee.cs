using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Employee), Schema = SchemaName.Worker)]
    public class Employee : AuditBase
    {

        public Employee()
        {
           //this.Remittances = new HashSet<Remittance>();
           // this.ShopStewards = new HashSet<ShopSteward>();
        }

        public int CorporateUnitId { get; set; }

        public int PersonId { get; set; }
        public string EmployeeNo { get; set; }

        public System.DateTime EmploymentDate { get; set; }

        public System.DateTime? TerminationDate { get; set; }

        public int EmploymentStatusId { get; set; }

        public bool IsPermanent { get; set; }

        public int CorporateUnitTradeId { get; set; }


        public virtual Person Person { get; set; }

        public virtual EmploymentStatus EmploymentStatus { get; set; }


        //public virtual ICollection<Remittance> Remittances { get; set; }

        //public virtual ICollection<ShopSteward> ShopStewards { get; set; }

    }
}
