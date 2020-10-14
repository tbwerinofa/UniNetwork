using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class EmailAccountViewModel : BaseViewModel
    {


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Email { get; set; }


        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string DisplayName { get; set; }


        [StringLength(50, ErrorMessage = "must be less than 50 characters")]
        [Required(ErrorMessage = "required")]
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }

        [Display(Name = "Use Default Credentials")]
        public bool UseDefaultCredentials { get; set; }

        [Display(Name = "Set As Default")]
        public bool IsDefaultAccount { get; set; }


    }
}
