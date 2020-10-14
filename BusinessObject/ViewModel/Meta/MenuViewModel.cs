using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class MenuViewModel : BaseViewModel
    {

        public int MenuGroupId { get; set; }
        public string MenuGroup { get; set; }
        public string MenuGroupIcon { get; set; }
        public string Icon { get; set; }
        public string Menu { get; set; }
        public string ActionResult { get; set; }
        public string Controller { get; set; }
        public string MenuSection { get; set; }

        public string MenuArea { get; set; }

        public string MenuAreaReference { get; set; }

        public bool ActiveMenuArea { get; set; }

        public int? MenuAreaOrdinal { get; set; }

        public Nullable<int> MenuGroupOrdinal { get; set; }
        public int MenuSectionOrdinal { get; set; }
        public int MenuOrdinal { get; set; }

        public string CurrentUserId { get; set; }

        public string DefaultMenu { get; set; }

        public int? DefaultMenuId { get; set; }

        public bool Navigable { get; set; }

        public string FullName { get; set; }
        public int MenuSectionId { get; set; }

        public string DefaultController { get; set; }
        public string DefaultActionResult { get; set; }

        public int? MenuAreaId { get; set; }

        public string Parameter { get; set; }
        public bool HasArea { get; set; }

        public string RoleId { get; set; }
        public string UserId { get; set; }

        public string[] ApplicationRoleIdList { get; set; }

        public IEnumerable<ApplicationRoleMenuViewModel> ApplicationRoleMenus { get; set; }
    }
}
