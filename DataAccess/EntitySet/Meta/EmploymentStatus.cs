using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(EmploymentStatus), Schema = SchemaName.Meta)]
    public class EmploymentStatus : AuditBase
    {
        public EmploymentStatus()
        {
            this.Employees = new HashSet<Employee>();
        }


        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}