using DataAccess.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    [Table(nameof(Control), Schema = SchemaName.Security)]
    public class Control : AuditBase
    {

        public Control()
        {
        }
        public string Type { get; set; }
        public string Value { get; set; }


    }
}
