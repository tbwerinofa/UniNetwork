using BusinessObject;
using BusinessObject.Component;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class MemberTransform
    {
        public static IEnumerable<MemberViewModel> ToListViewModel(
           this IQueryable<Member> entity)
        {
            return entity
                .Include(c => c.Person.Title)
                  .Include(c => c.Person.IDType)
                .Include(c => c.Person.Gender)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .AsNoTracking()
                .ToList().Select(a =>
                        new MemberViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            MemberNo = a.MemberNo,
                            NationalityId = a.Person.CountryId,
                            FullName = a.Person.FullName,
                            FirstName = a.Person.FirstName,
                            Surname = a.Person.Surname,
                            ContactNo = a.Person.ContactNo,
                            Email = a.Person.Email,
                            Initials = a.Person.Initials,
                            OtherName = a.Person.OtherName,
                            TitleId = a.Person.TitleId ?? 0,
                            GenderId = a.Person.GenderId,
                            IDNumber = a.Person.IDNumber,
                            IDTypeId = a.Person.IDTypeId,
                            IDType = a.Person.IDType.Name,
                            Gender = a.Person.Gender.Name,
                            BirthDateLongDate = a.Person.BirthDate.ToCustomLongDate(),
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }

        /// <summary>
        /// Convert Corporate Entity  into Corporate Object
        /// </summary>
        ///<param name="model">MemberViewModel</param>
        ///<param name="CorporateEntity">DataAccess.Corporate</param>
        ///<returns>MemberViewModel</returns>
        public static MemberViewModel ToViewModel(this Member entity,
            MemberViewModel model)
        {
            model.Id = entity.Id;
            model.MemberNo = entity.MemberNo;

            var person = entity.Person;
            model.NationalityId = person.CountryId;
            model.FirstName = person.FirstName;
            model.Surname = person.Surname;
            model.ContactNo = person.ContactNo;
            model.Email = person.Email;
            model.Initials = person.Initials;
            model.OtherName = person.OtherName;
            model.TitleId = person.TitleId ?? 0;
            model.GenderId = person.GenderId;
            model.IDNumber = person.IDNumber;
            model.IDTypeId = person.IDTypeId;
            model.AddressId = entity.Person.AddressId;
            model.BirthDate = entity.Person.BirthDate;
            model.BirthDateLongDate = entity.Person.BirthDate.ToCustomLongDate();
            model.Occupation = entity.Occupation;
            model.Company = entity.Company;
            model.WorkTelephone = entity.WorkTelephone;
            model.ContactNo = person.ContactNo;
            model.HomeTelephone = entity.HomeTelephone;
            model.EmmergencyContact1 = entity.EmmergencyContact1;
            model.EmmergencyContactNo1 = entity.EmmergencyContactNo1;
            model.EmmergencyContact2 = entity.EmmergencyContact2;
            model.EmmergencyContactNo2 = entity.EmmergencyContactNo2;
            model.Gender = entity.Person.Gender.Name;
            model.AgeGroup = entity.Person.AgeGroup.Name;
            model.DocumentName = entity.Person.Document?.Name;
            model.DocumentNameGuId = entity.Person.Document?.DocumentNameGuid;
            model.DocumentId = entity.Person?.DocumentId;
            model.MedicalAidNumber = entity.MedicalAidNumber;
            model.MedicalAidName = entity.MedicalAidName;
            return model;
        }


        /// <summary>
        /// Convert Member Object into Member Entity
        /// </summary>
        ///<param name="model">Member</param>
        ///<param name="MemberEntity">DataAccess.Member</param>
        ///<returns>DataAccess.Member</returns>
        public static DataAccess.Member ToEntity(this MemberViewModel model,
             DataAccess.Member entity)
        {
            if (entity.Id == 0)
            {
                entity.PersonId = model.PersonId;
                entity.MemberNo = model.MemberNo;
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }

            entity.Occupation = model.Occupation;
            entity.Company = model.Company;
            entity.WorkTelephone = model.WorkTelephone;
            entity.HomeTelephone = model.HomeTelephone;
            entity.EmmergencyContact1 = model.EmmergencyContact1;
            entity.EmmergencyContactNo1 = model.EmmergencyContactNo1;
            entity.EmmergencyContact2 = model.EmmergencyContact2;
            entity.EmmergencyContactNo2 = model.EmmergencyContactNo2;
            entity.MedicalAidNumber = model.MedicalAidNumber;
            entity.MedicalAidName = model.MedicalAidName;

            return entity;
        }

        public static IEnumerable<MemberListViewModel> ToQueryListViewModel(
           this IQueryable<Member> entity)
        {
            return entity
                .Include(c => c.Person.Title)
                .Include(c => c.Person.Gender)
                .Include(c => c.Person.AgeGroup)
                .Include(c => c.Person.Document)
                .Include(c => c.UpdatedUser)
                .Include(c => c.CreatedUser)
                .AsNoTracking()
                .ToList().Select(a =>
                        new MemberListViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            MemberNo = a.MemberNo,
                            NationalityId = a.Person.CountryId,
                            FullName = a.Person.FullName,
                            FirstName = a.Person.FirstName,
                            Surname = a.Person.Surname,
                            ContactNo = a.Person.ContactNo,
                            Email = a.Person.Email,
                            Initials = a.Person.Initials,
                            OtherName = a.Person.OtherName,
                            TitleId = a.Person.TitleId ?? 0,
                            GenderId = a.Person.GenderId,
                            IDNumber = a.Person.IDNumber,
                            IDTypeId = a.Person.IDTypeId,
                            AgeGroup = a.Person.AgeGroup.Name,
                            Gender = a.Person.Gender.Name,
                            DocumentName = a.Person.Document?.Name,
                            DocumentNameGuId = a.Person.Document?.DocumentNameGuid,
                            DocumentId = a.Person?.DocumentId,
                            BirthDateLongDate = a.Person.BirthDate.ToCustomLongDate(),
                            MedicalAidNumber = a.MedicalAidNumber,
                            MedicalAidName = a.MedicalAidName
            //LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
            //LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

        });
        }

        public static IEnumerable<DropDownListItems> ToDropDownListItem(
      this IQueryable<Member> entityList)
        {
            return entityList.Include(a => a.Person).Select(a => new DropDownListItems
            {
                Value = a.Id,
                Text = a.Person.FullName,
            })
            .OrderBy(a => a.Value2)
            .ThenBy(a => a.Text);
        }
    }

}
