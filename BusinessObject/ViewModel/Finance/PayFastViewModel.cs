using BusinessObject.Component;

namespace BusinessObject.ViewModel
{
    public class PayFastViewModel
    {

public string name_first {get;set;}
public string name_last {get;set;}
public string email_address {get;set;}
public string cell_number {get;set;}
public string m_payment_Id {get;set;}
public decimal amount {get;set;}
public string item_name {get;set;}
public string item_description {get;set;}
public int custom_int1 {get;set;}
public string custom_str1 {get;set;}
public int email_confirmation {get;set;}
public string confirmation_address {get;set;}
public string signature {get;set;}
        public string FullName { get; set; }
        public SaveResult SaveResult { get; set; }

        public MessageTemplateViewModel MessageTemplate { get; set; }
    }
}
