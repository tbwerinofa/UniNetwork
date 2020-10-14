using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessObject
{
    public class OrderViewModel : BaseViewModel
    {
        #region Fields

        public string OrderNo { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string Title { get; set; }

        public int TitleId { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        [DisplayName("Order Status")]
        public int OrderStatusId { get; set; }

        public int MemberId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public IEnumerable<SelectListItem> OrderStatuses { get; set; }

        [DisplayName("Date Created")]
        public string CreatedTimestampFormatted { get; set; }
        #endregion

        #region Navigation Properties

        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
        public string OrderStatus { get; set; }

        public OrderDetailViewModel OrderDetailViewModel { get; set; }
        #endregion


    }
}
