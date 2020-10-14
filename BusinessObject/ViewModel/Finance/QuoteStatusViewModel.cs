using BusinessObject.ViewModel;

namespace BusinessObject
{
    public class QuoteStatusViewModel : BaseViewModel
    {

        public string Name { get; set; }
        public string Discriminator { get; set; }

        public bool RequiresPayment { get; set; }

        public int MessageTemplateId { get; set; }

        public MessageTemplateViewModel MessageTemplate { get; set; }
    }
}
