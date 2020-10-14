using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CartViewModel:BaseViewModel
    {
        #region Fields

        public int RecordId { get; set; }

        public string CartId { get; set; }

        public int ProductId { get; set; }

        public int SizeId { get; set; }

        public int ProductSizeId { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ProductViewModel Product { get; set; }

        public virtual SizeViewModel Size { get; set; }
        public string ProductName { get; set; }

        #endregion
    }
}
