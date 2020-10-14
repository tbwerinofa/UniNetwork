using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class EmployeeViewModel : BaseViewModel
    {
        public string EmployeeNo { get; set; }

        [Display(Name = "Is Permanent")]
        public bool IsPermanent { get; set; }

        public int CorporateUnitId { get; set; }

        public int CorporateUnitTradeId { get; set; }


        public IEnumerable<SelectListItem> EmployeeStatuses { get; set; }

        [Display(Name = "Employment Status")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]

        public int EmploymentStatusId { get; set; }

        public DateTime EmploymentDate { get; set; }
        public string EmploymentDateLongDate { get; set; }


        [DisplayName("Termination Date")]
        [DataType(DataType.Date)]
 
        public DateTime? TerminationDate { get; set; }
        public string TerminationDateLongDate { get; set; }

        public int PersonId { get; set; }
        public PersonViewModel Person { get; set; }
    }
}