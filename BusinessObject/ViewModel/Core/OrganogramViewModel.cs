using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject.ViewModel
{
    public class OrganogramViewModel : BaseViewModel
    {

        [Display(Name = "Structure Level")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int StructureLevelId { get; set; }


        [Display(Name = "Structure Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int StructureTypeId { get; set; }

        [Display(Name = "Structure")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int StructureId { get; set; }

        public string StructureLevel { get; set; }

        public string StructureType { get; set; }

        public string Structure { get; set; }

        public IEnumerable<SelectListItem> StructureLevels { get; set; }

        public IEnumerable<SelectListItem> StructureTypes { get; set; }

        public IEnumerable<SelectListItem> Structures { get; set; }

    }
}

