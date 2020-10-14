using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RaceOrganisation), Schema = SchemaName.Activity)]

    public partial class RaceOrganisation:AuditBase
    {

        public RaceOrganisation()
        {
           
        }
        public int RaceId { get; set; }
        public int OrganisationId { get; set; }
        public virtual Race Race { get; set; }
        public virtual Organisation Organisation { get; set; }
    
    }
}
