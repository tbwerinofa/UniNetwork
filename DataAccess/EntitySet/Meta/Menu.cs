using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Menu), Schema = SchemaName.Meta)]
    public partial class Menu : AuditBase
    {
        public Menu()
        {
            this.ApplicationRoleMenus = new HashSet<ApplicationRoleMenu>();
            this.DefaultMenuAreas = new HashSet<MenuArea>();
        }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string ActionResult { get; set; }
        public string Icon { get; set; }
        public string Parameter { get; set; }
        public Nullable<int> MenuAreaId { get; set; }
        public int MenuSectionId { get; set; }
        public int Ordinal { get; set; }
        public bool HasArea { get; set; }
        public virtual MenuArea MenuArea { get; set; }
        public virtual MenuSection MenuSection { get; set; }
        public virtual ICollection<MenuArea> DefaultMenuAreas { get; set; }

        public virtual ICollection<ApplicationRoleMenu> ApplicationRoleMenus { get; set; }
    }

}