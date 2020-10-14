using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class FeaturedCategoryViewModel : BaseViewModel
    {

        [Required(ErrorMessage = "required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required.")]
        public int Ordinal { get; set; }

        public IEnumerable<FeaturedImageViewModel> FeaturedImages { get; set; }

    }
}