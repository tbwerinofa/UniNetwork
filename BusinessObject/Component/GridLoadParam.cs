using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.Component
{
    public class GridLoadParam
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsDescending { get; set; }

        public string SortField { get; set; }

        public string SearchTerm { get; set; }

        public int? RowId;

        public bool IsValid { get; set; }

        public string SessionUserId { get; set; }
        public int? ParentId { get; set; }

        public bool? IsFeatured { get; set; }


        public int? ProductCategoryId { get; set; }

        public int SortCategoryId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public AuthorizationModel AuthorizationFilter { get; set; }
    }
}
