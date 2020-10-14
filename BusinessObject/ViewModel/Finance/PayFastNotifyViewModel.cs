using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
        public class PayFastNotifyViewModel
    {

        public int Id{ get; set; }
        public Nullable<int> m_payment_id { get; set; }
        public int pf_payment_id { get; set; }
        public string payment_status { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public decimal amount_gross { get; set; }
        public decimal amount_fee { get; set; }
        public decimal amount_net { get; set; }
        public int custom_int1 { get; set; }
        public int custom_str1 { get; set; }
        public string name_first { get; set; }
        public string name_last { get; set; }
        public string email_address { get; set; }
        public string fullname { get; set; }
        public string CreatedTimestamp { get; set; }
        public string UpdatedTimestamp { get; set; }
        public bool IsDeleted { get; set; }

    }
}
