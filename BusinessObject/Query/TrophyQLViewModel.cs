using BusinessObject.Component;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class TrophyQLViewModel : BaseViewModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public DocumentViewModel Document { get; set; }

        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }
        public string DocumentExtension { get; set; }

        public string DocumentGuid { get; set; }

        public int? DocumentId { get; set; }


        public IFormFile FileUploaded { get; set; }

        public string ExpiryDateMaxLongDate { get; set; }

    }
}

