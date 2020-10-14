using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class MessageTemplateViewModel : BaseViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [EmailAddress(ErrorMessage = "invalid email format.")]
        public string BccEmailAddresses { get; set; }

        [Required(ErrorMessage = "required")]
        public string Subject { get; set; }

        // [AllowHtml]
        [Required(ErrorMessage = "required")]
        [StringLength(5000, ErrorMessage = "must be less than 5000 characters")]
        public string Body { get; set; }
        public Nullable<int> DelayBeforeSend { get; set; }

        [DisplayName("Delay Hours")]
        public int DelayHours { get; set; }
        public int AttachedDownloadId { get; set; }
        public bool LimitedToApplications { get; set; }

        [DisplayName("Date Created")]
        public string CreatedTimestamp { get; set; }

        [Display(Name = "Email Account")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int EmailAccountId { get; set; }

        public IEnumerable<SelectListItem> EmailAccounts { get; set; }

        [Display(Name = "Role")]
        public IEnumerable<string> RoleIds { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public string MessageTokens { get; set; }

        [Required(ErrorMessage = "required")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters")]
        [EmailAddress(ErrorMessage = "invalid email format.")]
        public string FromAddress { get; set; }


    }
}
