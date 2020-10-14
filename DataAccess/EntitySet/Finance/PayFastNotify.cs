using DataAccess.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(PayFastNotify), Schema = SchemaName.Finance)]
    public partial class PayFastNotify
    {
        public int Id { get; set; }
        public Nullable<int> M_payment_id { get; set; }
        public int Pf_payment_id { get; set; }
        public string Payment_status { get; set; }
        public string Item_name { get; set; }
        public string Item_description { get; set; }
        public decimal Amount_gross { get; set; }
        public decimal Amount_fee { get; set; }
        public decimal Amount_net { get; set; }
        public int Custom_int1 { get; set; }
        public int Custom_str1 { get; set; }
        public string Name_first { get; set; }
        public string Name_last { get; set; }
        public string Email_address { get; set; }
        public bool Isprocessed { get; set; }
        public System.DateTime CreatedTimestamp { get; set; }
        public Nullable<System.DateTime> UpdatedTimestamp { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Quote Quote { get; set; }
    }
}
