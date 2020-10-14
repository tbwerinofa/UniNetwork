using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Country), Schema = SchemaName.Gis)]
    public class Country : AuditBase
    {

        public Country()
        {
            this.Provinces = new HashSet<Province>();
            this.MemberStagings = new HashSet<MemberStaging>();
            //this.People = new HashSet<Person>();
        }

        public string Name { get; set; }
        public string Code { get; set; }

        public int GlobalRegionId { get; set; }

        public virtual GlobalRegion GlobalRegion { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }

        public virtual ICollection<MemberStaging> MemberStagings { get; set; }
       //public virtual ICollection<Person> People { get; set; }

    }
}