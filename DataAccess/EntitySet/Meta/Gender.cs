using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Gender), Schema = SchemaName.Meta)]
    public class Gender:AuditBase
    {
        public Gender()
        {
            //this.People = new HashSet<Person>();
            this.MemberStagings = new HashSet<MemberStaging>();
            //this.Awards = new HashSet<Award>();
        }


        public string Name { get; set; }

        public string Discriminator { get; set; }

        //public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<MemberStaging> MemberStagings { get; set; }
        //public virtual ICollection<Award> Awards { get; set; }
    }
}