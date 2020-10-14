using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(IDType), Schema = SchemaName.Meta)]
    public partial class IDType : AuditBase
    {
        public IDType()
        {
            //this.People = new HashSet<Person>();
            this.MemberStagings = new HashSet<MemberStaging>();
        }
        public string Name { get; set; }
        public virtual ICollection<MemberStaging> MemberStagings { get; set; }
        //public virtual ICollection<Person> People { get; set; }
    }

}