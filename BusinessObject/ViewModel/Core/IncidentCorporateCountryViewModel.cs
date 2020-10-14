using BusinessObject.Component;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ViewModel
{
    public class IncidentCorporateCountryViewModel : BaseViewModel
    {

        public string MNCCountry { get; set; }

        public string Incident { get; set; }
        public int MNCCountryId { get; set; }
        public int IncidentId { get; set; }

        public IEnumerable<int> CorporateUnitIds { get; set; }
    }
}
