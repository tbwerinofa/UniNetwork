using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class MenuAreaViewModel
    {
        public int ID { get; set; }
        public string Menu { get; set; }
        public string DefaultActionResult { get; set; }
        public string DefaultController { get; set; }

        public string MenuArea { get; set; }

        public string MenuAreaReference { get; set; }

        public int? MenuAreaOrdinal { get; set; }

        public string DefaultMenu { get; set; }

        public bool Navigable { get; set; }

        public int? DefaultMenuID { get; set; }

    }
}
