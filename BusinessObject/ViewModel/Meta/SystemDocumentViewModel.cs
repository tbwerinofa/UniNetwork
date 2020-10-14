using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class SystemDocumentViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Upload File")]
        [BindProperty]
        public IFormFile FileUploaded { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Order")]
        public int Ordinal { get; set; }

        public string DocumentName { get; set; }
        public string AnchorTagName { get; set; }
        public string DocumentPath { get; set; }

        public string DocumentExtension { get; set; }

        public string DocumentNameGuId { get; set; }

        public int DocumentId { get; set; }

        [Display(Name = "Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Year")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int FinYearId { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        public IEnumerable<SelectListItem> FinYears { get; set; }
        public int FinYear { get; set; }
        public string DocumentType { get; set; }
    }
}
