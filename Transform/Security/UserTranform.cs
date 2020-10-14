using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class UserTransform
    {
        /// <summary>
        /// Convert MNC Object into MNC Entity
        /// </summary>
        ///<param name="model">MNC</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>DataAccess.MNC</returns>
        public static ApplicationUser ToEntity(this UserViewModel model, ApplicationUser entity
           )
        {
            entity.UserName = model.Email;
            entity.Email = model.Email;
            entity.FirstName = model.FirstName;
            entity.Surname = model.Surname;
            entity.ContactNo = model.ConfirmEmail;
            entity.LockoutEnabled = !model.IsActive;
            return entity;
        }

        public static IEnumerable<UserViewModel> ToListViewModel(
           this IQueryable<ApplicationUser> entity)
        {
            return entity
                .AsNoTracking()
                .ToList().Select(a =>
                        new UserViewModel
                        {
                            Id = a.Id,
                            Email = a.Email,
                            UserName = a.UserName,
                            EmailConfirmed = a.EmailConfirmed,
                           ContactNo = a.ContactNo,
                            FirstName = a.FirstName,
                            Surname = a.Surname, 
                            FullName = a.FullName,
                        });
        }

        /// <summary>
        /// Convert MNC Entity  into MNC Object
        /// </summary>
        ///<param name="model">AgreementViewModel</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>AgreementViewModel</returns>
        public static UserViewModel ToViewModel(this ApplicationUser entity,
            UserViewModel model)
        {
            model.Id = entity.Id;
            model.UserName = entity.UserName;
            model.Email = entity.Email;
            model.ConfirmEmail = entity.Email;
            model.FirstName = entity.FirstName;
            model.Surname = entity.Surname;
            model.FullName = entity.FullName;
            model.ContactNo = entity.ContactNo;
            model.IsActive = entity.LockoutEnabled;

            if (entity.UserRegions != null && entity.UserRegions.Any())
            {
                model.CountryId = entity.UserRegions.First().Region.CountryId;
                model.RegionIds = entity.UserRegions.Select(a => a.RegionId);
                model.Regions = entity.UserRegions.Select(b => b.Region).AsQueryable().ToSelectListItem(a => a.Name.ToString(), a => a.Id.ToString());
            }


            return model;
        }
    }
}
