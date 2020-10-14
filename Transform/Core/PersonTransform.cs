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
    public static class PersonTransform
    {

        /// <summary>
        /// Convert Person Object into Person Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Person</param>
        ///<returns>IEnumerable Person</returns>
        ///
        public static IEnumerable<PersonViewModel> ToListViewModel(
            this IQueryable<DataAccess.Person> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new PersonViewModel
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            Surname = a.Surname,
                            GenderId = a.GenderId,
                            TitleId = a.TitleId??0,
                            Gender = a.Gender.Name,
                            Title = a.Title.Name,
                            Initials = a.Initials,
                            OtherName = a.OtherName,
                            ContactNo = a.ContactNo,
                            Email = a.Email,
                            PersonGuid = a.PersonGuid,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName,
                        });
        }

        /// <summary>
        /// Convert Person Object into Person Entity
        /// </summary>
        ///<param name="model">Person</param>
        ///<param name="PersonEntity">DataAccess.Person</param>
        ///<returns>DataAccess.Person</returns>
        public static DataAccess.Person ToEntity(this PersonViewModel model,
             DataAccess.Person entity)
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

            entity.IDNumber = model.IDNumber;
            entity.CountryId = model.CountryId;
            entity.Initials = model.Initials;
            entity.OtherName = model.OtherName;
            entity.IDTypeId = model.IDTypeId;
            entity.ContactNo = model.ContactNo;
            entity.Email = model.Email;
            entity.Surname = model.Surname;
            entity.FirstName = model.FirstName;
            entity.Surname = model.Surname;
            entity.TitleId = model.TitleId;
            entity.GenderId = model.GenderId;

            return entity;
        }

        /// <summary>
        /// Convert Person Entity  into Person Object
        /// </summary>
        ///<param name="model">PersonViewModel</param>
        ///<param name="PersonEntity">DataAccess.Person</param>
        ///<returns>PersonViewModel</returns>
        public static PersonViewModel ToViewModel(
         this DataAccess.Person entity,
         PersonViewModel model)
        {
            model.Id = entity.Id;
            model.SessionUserId = entity.CreatedUserId;
            model.Title = entity.Title.Name;
            model.Gender = entity.Gender.Name;
            model.FirstName = entity.FirstName;
            model.Surname = entity.Surname;
            model.ContactNo = entity.ContactNo;
            model.Email = entity.Email;
            model.TitleId = entity.TitleId??0;
            model.GenderId = entity.GenderId;
            model.Initials = entity.Initials;
            model.OtherName = entity.OtherName;
            model.IDTypeId = entity.IDTypeId;
            model.IDNumber = entity.IDNumber;
            model.CountryId = entity.CountryId;
            model.PersonGuid = entity.PersonGuid;
            return model;
        }

    }

}
