using System;
using System.Collections.Generic;
using DataAccess.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess
{
    [Table(nameof(RunningCategory), Schema = SchemaName.Activity)]
    public partial class RunningCategory : AuditBase
    {
        
        public RunningCategory()
        {

        }

        public string Name { get; set; }
 
    }
}
