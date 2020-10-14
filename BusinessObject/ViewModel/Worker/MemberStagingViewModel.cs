using BusinessObject.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class MemberStagingViewModel : BaseViewModel
    {

        public int AddressId { get; set; }

        public AddressViewModel Address { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "first name is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "surname is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string Surname { get; set; }

        [Display(Name = "ID Number")]
        [Required(ErrorMessage = "ID number is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "must be equal to 13 characters")]
        public string IDNumber { get; set; }

        [Display(Name = "Alternate ID")]
        [Required(ErrorMessage = "alternate id isrequired")]
        [StringLength(20, ErrorMessage = "must be less than characters")]
        public string AlternateIDNumber { get; set; }

        public string Initials { get; set; }
        public string IDType { get; set; }

        [Display(Name = "Other name")]
        public string OtherName { get; set; }

        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Work Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string WorkTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Home Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string HomeTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cell Phone")]
        [Required(ErrorMessage = "cell is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string CellPhone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string Email { get; set; }

        [Display(Name = "Emergency Contact Detail person 1")]
        [Required(ErrorMessage = "emergency contact detail person 1 is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string EmmergencyContact1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Emergency Contact Number 1")]
        [Required(ErrorMessage = "emergency contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string EmmergencyContactNo1 { get; set; }

        [Display(Name = "Emergency Contact Detail person 2")]
        [Required(ErrorMessage = "emergency contact detail person 2 is required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string EmmergencyContact2 { get; set; }

        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string MedicalAidName { get; set; }

        [Display(Name = "Number")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string MedicalAidNumber { get; set; }

        [Display(Name = "Previous Year")]
        [StringLength(10, ErrorMessage = "must be less than 10 characters.")]
        public string PrevYearLicence { get; set; }

        [Display(Name = "Current Year")]
        [StringLength(10, ErrorMessage = "must be less than 10 characters.")]
        public string CurrentYearLicence { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "contact number required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string EmmergencyContactNo2 { get; set; }


        [Display(Name = "Gender")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "gender is required")]
        public int GenderId { get; set; }

        [Display(Name = "Title")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "title is required")]
        public int TitleId { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "date of birth is required")]
        public DateTime BirthDate { get; set; }

        public string BirthDateLongDate { get; set; }

        public string FullName { get; set; }

        public string PersonGuid { get; set; }
        [Display(Name = "ID Type")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "ID type required")]
        public int IDTypeId { get; set; }

        [Display(Name = "Nationality")]
        [Required, Range(1, Int32.MaxValue, ErrorMessage = "nationality is required")]
        public int NationalityId { get; set; }


        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> Titles { get; set; }

        public IEnumerable<SelectListItem> Nationalities { get; set; }


        public IEnumerable<SelectListItem> IDTypes { get; set; }

        public IEnumerable<UserViewModel> NotificationRoles { get; set; }


        [Display(Name = "Do you want to approve this application?")]
        public bool IsFinalised { get; set; }

        public bool IsFromFront{ get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "must be less than 500 characters.")]
        public string Comments { get; set; }

        public int? PersonId { get; set; }
        public string RequestUrl { get; set; }
    }
}
