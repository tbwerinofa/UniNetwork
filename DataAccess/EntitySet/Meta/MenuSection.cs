using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(MenuSection), Schema = SchemaName.Meta)]
    public partial class MenuSection : AuditBase
    {
        public MenuSection()
        {
            this.Menus = new HashSet<Menu>();
        }

        public string Name { get; set; }
        public int Ordinal { get; set; }
        public int MenuGroupId { get; set; }
       
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual MenuGroup MenuGroup { get; set; }

    }

}