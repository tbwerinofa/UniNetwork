using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Title), Schema = SchemaName.Meta)]
    public class Title:AuditBase
    {
        public Title()
        {
            //this.People = new HashSet<Person>();
            this.MemberStagings = new HashSet<MemberStaging>();
        }


        public string Name { get; set; }

        //public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<MemberStaging> MemberStagings { get; set; }
    }
}