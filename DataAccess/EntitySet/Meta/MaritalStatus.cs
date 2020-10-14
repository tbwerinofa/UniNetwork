using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(MaritalStatus), Schema = SchemaName.Meta)]
    public partial class MaritalStatus : AuditBase
    {
        public MaritalStatus()
        {

        }
        public string Name { get; set; }

    }

}