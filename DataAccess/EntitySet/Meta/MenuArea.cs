using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(MenuArea), Schema = SchemaName.Meta)]
    public partial class MenuArea : AuditBase
    {
        public MenuArea()
        {
            this.Menus = new HashSet<Menu>();
        }

        public string Name { get; set; }
        public Nullable<int> DefaultMenuID { get; set; }
        public string Discriminator { get; set; }
        public bool Navigable { get; set; }
        public int Ordinal { get; set; }

        public virtual Menu DefaultMenu { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }

    }

}