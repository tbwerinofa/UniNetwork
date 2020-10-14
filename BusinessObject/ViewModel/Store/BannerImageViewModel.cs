using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class BannerImageViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Upload File")]
        [BindProperty]
        public IFormFile FileUploaded { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Order")]
        public int Ordinal { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentExtension { get; set; }

        public string DocumentNameGuId { get; set; }

        public int DocumentId { get; set; }


    }
}
