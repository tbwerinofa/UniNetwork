using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Ethnic), Schema = SchemaName.Meta)]
    public class Ethnic : AuditBase
    {
        public Ethnic()
        {

        }

        public string Name { get; set; }


    }
}