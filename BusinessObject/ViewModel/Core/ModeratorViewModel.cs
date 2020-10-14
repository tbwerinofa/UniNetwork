using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class ModeratorViewModel : BaseViewModel
    {

        public int MemberId { get; set; }

        public int CalendarId { get; set; }
        public string FullName { get; set; }
    }
}
