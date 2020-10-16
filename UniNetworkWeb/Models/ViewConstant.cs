using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniNetworkWeb.Models
{
    public static class ModelConstant
    {
        public static object FormControlInp = new { @class = "form-control input-sm" };
        public static object FormControlInpMultiple = new { @class = "form-control input-sm", @multiple = "multiple" };
        public static object LabelRequired = new { @class = "control-label col-4 input-sm field-required" };
        public static object LabelStandard = new { @class = "control-label col-4 input-sm" };
        public static object FormControlInpNumberOnly = new { @class = "form-control input-sm number-only" };
    }
}
