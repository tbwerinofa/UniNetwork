using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(MenuGroup), Schema = SchemaName.Meta)]
    public partial class MenuGroup : AuditBase
    {
        public MenuGroup()
        {
            this.MenuSections = new HashSet<MenuSection>();
        }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Ordinal { get; set; }
        public virtual ICollection<MenuSection> MenuSections { get; set; }
    }

}