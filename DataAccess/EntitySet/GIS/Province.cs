using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Province), Schema = SchemaName.Gis)]
    public class Province:AuditBase
    {
        public Province()
        {
            //this.UserRegions = new HashSet<UserRegion>();
            //this.RaceDefinitions = new HashSet<RaceDefinition>();
            //this.Organisations = new HashSet<Organisation>();
            this.Cities = new HashSet<City>();
        }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        //public virtual ICollection<UserRegion> UserRegions { get; set; }
        //public virtual ICollection<RaceDefinition> RaceDefinitions { get; set; }

        //public virtual ICollection<Organisation> Organisations { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}