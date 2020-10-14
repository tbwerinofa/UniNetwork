using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject
{
    public class ShopStewardSettingViewModel : BaseViewModel
    {

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "required")]
        public DateTime StartDate { get; set; }

        public string StartDateLongDate { get; set; }

        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int ValidYears { get; set; }

    }
}
