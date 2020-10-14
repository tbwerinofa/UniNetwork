using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ShoppingCartViewModel
    {
        public List<CartViewModel> CartItems { get; set; }

        public decimal CartTotal { get; set; }

        public decimal TotalExcludingVAT { get; set; }

        public decimal VAT { get; set; }

        public int CartCount { get; set; }

        public string DocumentPath { get; set; }
    }
}
