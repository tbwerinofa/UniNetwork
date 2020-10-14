using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public class AuthorizationModel
    {
        public string UserId { get; set; }
        public int? PersonId { get; set; }

        public int? MemberId { get; set; }
        public IEnumerable<int> CorporateUnitIds { get; set; }

        public IEnumerable<int> RegionIds { get; set; }

    }

}
