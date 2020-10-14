using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(SortCategory), Schema = SchemaName.Store)]
    public partial class SortCategory : AuditBase
    {
        public SortCategory()
        {
        }

        public string Name { get; set; }
        public string Discriminator { get; set; }
        public int Ordinal { get; set; }


    }
}
