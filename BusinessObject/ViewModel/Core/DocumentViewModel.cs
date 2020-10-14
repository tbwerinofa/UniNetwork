using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.ViewModel
{
    public class DocumentViewModel : BaseViewModel
    {


        public string Name { get; set; }

        public string DocumentType { get; set; }

        public string Path { get; set; }

        public string DocumentNameGuid { get; set; }

        public int DocumentTypeId { get; set; }

        public byte[] DocumentData { get; set; }
        /// <summary>
        /// Gets or sets Notes
        /// </summary>
        public string Extension { get; set; }

        public string Comments { get; set; }

  


    }
}
