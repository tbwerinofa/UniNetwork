using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Language), Schema = SchemaName.Meta)]
    public partial class Language : AuditBase
    {
        public Language()
        {

        }
        public string Name { get; set; }

    }

}