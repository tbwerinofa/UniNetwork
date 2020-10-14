using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class BaseViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string SessionUserId { get; set; }

        public string CreatedUser { get; set; }

        public string LastUpdatedUser { get; set; }

        public string CreatedDate { get; set; }

        public string LastUpdate { get; set; }


    }
}
