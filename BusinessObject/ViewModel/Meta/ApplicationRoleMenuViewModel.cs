using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class ApplicationRoleMenuViewModel : BaseViewModel
    {

        public int ApplicationRoleId { get; set; }
        public int MenuId { get; set; }

        public string ApplicationRole { get; set; }
        public string Menu { get; set; }

    }
}
