using BusinessObject;
using BusinessObject.Component;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class MemberStagingTransform
    {
        /// <summary>
        /// Convert Corporate Object into Corporate Entity
        /// </summary>
        ///<param name="model">Corporate</param>
        ///<param name="CorporateEntity">DataAccess.Corporate</param>
        ///<returns>DataAccess.Corporate</returns>
        public static MemberStaging ToEntity(this MemberStagingViewModel model,MemberStaging entity
           )
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
           
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.FirstName = model.FirstName;
            entity.Surname = model.Surname;
            entity.ContactNo = model.CellPhone;
            entity.Email = model.Email;
            entity.Initials = model.Initials;
            entity.OtherName = model.OtherName;
            entity.TitleId = model.TitleId;
            entity.BirthDate = model.BirthDate;
            entity.GenderId = model.GenderId;

            if (model.IDTypeId == 1)
            {
                entity.IDNumber = model.IDNumber;
            }
            else
            {
                entity.IDNumber = model.AlternateIDNumber;
            }
            entity.IDTypeId = model.IDTypeId;
            entity.CountryId = model.NationalityId;
            entity.AddressId = model.AddressId;
            entity.IsFromFront = model.IsFromFront;
            entity.IsFinalised = model.IsFinalised;
            entity.Occupation = model.Occupation;
            entity.Company = model.Company;
            entity.WorkTelephone = model.WorkTelephone;
            entity.HomeTelephone = model.HomeTelephone;
            entity.EmmergencyContact1 = model.EmmergencyContact1;
            entity.EmmergencyContactNo1 = model.EmmergencyContactNo1;
            entity.EmmergencyContact2 = model.EmmergencyContact2;
            entity.EmmergencyContactNo2 = model.EmmergencyContactNo2;
            entity.MedicalAidName = model.MedicalAidName;
            entity.MedicalAidNumber = model.MedicalAidNumber;
            entity.PrevYearLicence = model.PrevYearLicence;
            entity.CurrentYearLicence = model.CurrentYearLicence;
            return entity;
        }

        public static IEnumerable<MemberStagingViewModel> ToListViewModel(
           this IQueryable<MemberStaging> entity)
        {
            return entity
                .Include(c => c.Title)
                 .Include(c => c.IDType)
                .Include(c => c.Gender)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .AsNoTracking()
                .ToList().Select(a =>
                        new MemberStagingViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            NationalityId = a.CountryId,
                            //FullName = a.FullName,
                            FirstName = a.FirstName,
                            CellPhone = a.ContactNo,
                            Surname = a.Surname,
                            HomeTelephone = a.ContactNo,
                            Email = a.Email,
                            Initials = a.Initials,
                            OtherName = a.OtherName,
                            TitleId = a.TitleId??0,
                            GenderId = a.GenderId,
                            IDNumber = a.IDNumber,
                            IDTypeId = a.IDTypeId,
                            IDType = a.IDType.Name,
                            Gender = a.Gender.Name,
                            AddressId = a.AddressId,
                            IsFinalised = a.IsFinalised,
                            IsFromFront = a.IsFromFront,
                            BirthDate = a.BirthDate,
                            BirthDateLongDate = a.BirthDate.ToCustomLongDate(),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            //LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                         
                        });
        }

        /// <summary>
        /// Convert Corporate Entity  into Corporate Object
        /// </summary>
        ///<param name="model">MemberStagingViewModel</param>
        ///<param name="CorporateEntity">DataAccess.Corporate</param>
        ///<returns>MemberStagingViewModel</returns>
        public static MemberStagingViewModel ToViewModel(this MemberStaging entity,
            MemberStagingViewModel model)
        {
            model.Id = entity.Id;
           model.NationalityId = entity.CountryId;
           model.FirstName = entity.FirstName;
           model.Surname = entity.Surname;
           model.CellPhone = entity.ContactNo;
           model.Email = entity.Email;
           model.Initials = entity.Initials;
           model.OtherName = entity.OtherName;
           model.TitleId = entity.TitleId ?? 0;
           model.GenderId = entity.GenderId;
           model.IDNumber = entity.IDNumber;
           model.IDTypeId = entity.IDTypeId;
           model.AddressId = entity.AddressId;
            model.AddressId = entity.AddressId;
            model.BirthDate = entity.BirthDate;
            model.IsFromFront = entity.IsFromFront;
           // model.FullName = entity.FullName;
            model.BirthDateLongDate = entity.BirthDate.ToCustomLongDate();
            model.Occupation = entity.Occupation;
            model.Company = entity.Company;
            model.WorkTelephone = entity.WorkTelephone;
            model.HomeTelephone = entity.HomeTelephone;
            model.EmmergencyContact1 = entity.EmmergencyContact1;
            model.EmmergencyContactNo1 = entity.EmmergencyContactNo1;
            model.EmmergencyContact2 = entity.EmmergencyContact2;
            model.EmmergencyContactNo2 = entity.EmmergencyContactNo2;
            model.PrevYearLicence = entity.PrevYearLicence;
            model.CurrentYearLicence = entity.CurrentYearLicence;
            model.MedicalAidNumber = entity.MedicalAidNumber;
            model.MedicalAidName = entity.MedicalAidName;


            return model;
        }


    }
}
