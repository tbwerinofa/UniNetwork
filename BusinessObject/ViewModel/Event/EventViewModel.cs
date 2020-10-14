using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class EventViewModel: BaseViewModel
    {
        

        [Display(Name = "Event")]
        [Required(ErrorMessage = "required")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters.")]
        public string Name { get; set; }

        [Display(Name = "Requires RSVP")]
        public bool RequiresRsvp { get; set; }

        [Display(Name = "Requires Subscription")]

        public bool RequiresSubscription { get; set; }

        [Display(Name = "Frequency")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FrequencyId { get; set; }

        [Display(Name = "Event Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int EventTypeId { get; set; }

        public IEnumerable<SelectListItem> EventTypes { get; set; }

        public IEnumerable<SelectListItem> Frequencies { get; set; }

        public string EventType { get; set; }

        public string Frequency { get; set; }

        public IEnumerable<CalendarViewModel> Calendars { get; set; }
}
}
