using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class MemberLicenseViewModel : BaseViewModel
    {

        [Display(Name = "License No")]
        [StringLength(10, ErrorMessage = "must be less than 50 characters.")]
        [Required(ErrorMessage = "required")]
        public string LicenseNo { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "year is required")]
        public int FinYearId { get; set; }

        [Display(Name = "Member")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "member is required")]
        public int MemberId { get; set; }

        public IEnumerable<SelectListItem> FinYears { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; }
        public int FinYear { get; set; }
        public string MemberNo { get; set; }
        public string FullName { get; set; }
    }
}
