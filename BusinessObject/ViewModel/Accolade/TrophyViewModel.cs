using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TrophyViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "required")]
        [StringLength(50, ErrorMessage = "must be less than 50 characters.")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(250, ErrorMessage = "must be less than 250 characters.")]
        public string Description { get; set; }

        public DocumentViewModel Document { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }
        public string DocumentExtension { get; set; }

        public string DocumentGuid { get; set; }

        public int? DocumentId { get; set; }

        [Display(Name = "Upload File")]
        public IFormFile FileUploaded { get; set; }

        public string ExpiryDateMaxLongDate { get; set; }

        public IEnumerable<WinnerViewModel> Winners { get; set; }

    }
}

