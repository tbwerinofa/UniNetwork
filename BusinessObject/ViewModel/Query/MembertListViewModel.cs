using BusinessObject.Component;
using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObject
{
    public class MemberListViewModel : BaseViewModel
    {

        public string MemberNo { get; set; }
        public int PersonId { get; set; }


        public AddressViewModel Address { get; set; }

        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }

        public string Initials { get; set; }


        [Display(Name = "Other name")]
        public string OtherName { get; set; }

        [Display(Name = "EmployeeNo")]
        public string EmployeeNo { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string ContactNo { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Gender")]
        public int GenderId { get; set; }

        [Display(Name = "Title")]
        public int TitleId { get; set; }


        public DateTime BirthDate { get; set; }

        public string BirthDateLongDate { get; set; }

        public string FullName { get; set; }

        [Display(Name = "ID Type")]
        public int IDTypeId { get; set; }

        [Display(Name = "Nationality")]
        public int NationalityId { get; set; }


        [Display(Name = "Age Group")]
        public int AgeGroupId { get; set; }

        [Display(Name = "Medical Aid")]
        public string MedicalAidName { get; set; }

        [Display(Name = "Medical Aid Number")]
        public string MedicalAidNumber { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> AgeGroups { get; set; }      

        [Display(Name = "Operational Country")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "required")]
        public int CountryId { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public PersonViewModel Person { get; set; }

        public EmployeeViewModel Employee { get; set; }
        public string AgeGroup { get; set; }

        public string DocumentName { get; set; }

        public string DocumentPath { get; set; }

        public string DocumentNameGuId { get; set; }

        public int? DocumentId { get; set; }
    }
}

