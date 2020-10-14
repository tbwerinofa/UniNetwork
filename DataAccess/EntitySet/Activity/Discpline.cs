using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{

    [Table(nameof(Discpline), Schema = SchemaName.Activity)]

    public partial class Discpline:AuditBase
    {

        public Discpline()
        {
            this.RaceDefinitions = new HashSet<RaceDefinition>();
        }
    
        public string Name { get; set; }
        public virtual ICollection<RaceDefinition> RaceDefinitions { get; set; }
    }
}
