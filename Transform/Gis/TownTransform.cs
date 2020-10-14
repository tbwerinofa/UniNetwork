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
    public static class TownSuburbTransform
    {


        /// <summary>
        /// Convert Town Object into Town Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Town</param>
        ///<returns>IEnumerable Town</returns>
        ///
        public static IEnumerable<TownViewModel> ToListViewModel(
            this IQueryable<DataAccess.Town> entity)
        {
            return entity
                .Include(c => c.City)
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new TownViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CityId = a.CityId,
                            City = a.City.Name,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Town Object into Town Entity
        /// </summary>
        ///<param name="model">Town</param>
        ///<param name="TownEntity">DataAccess.Town</param>
        ///<returns>DataAccess.Town</returns>
        public static Town ToEntity(this TownViewModel model,
            Town entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.IsActive = model.IsActive;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.CityId = model.CityId;
            entity.Name = model.Name;
            return entity;
        }

        /// <summary>
        /// Convert Town Entity  into Town Object
        /// </summary>
        ///<param name="model">TownViewModel</param>
        ///<param name="TownEntity">DataAccess.Town</param>
        ///<returns>TownViewModel</returns>
        public static TownViewModel ToViewModel(
         this DataAccess.Town entity,
         TownViewModel model)
        {
            model.Id = entity.Id;
            model.CityId = entity.CityId;
            model.Name = entity.Name;
            model.City = entity.City.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

        /// <summary>
        /// Transform IQueryable DataAccess.TownSuburb into IEnumerable DropDownListItems
        /// </summary>
        ///<param name="gisTownSuburb">IQueryable DataAccess.TownSuburb</param>
        ///<returns>IEnumerable DropDownListItems</returns>
        public static IEnumerable<DropDownListItems> ToTownDropDownListItems(
            this IQueryable<DataAccess.Town> gisTownSuburb)
        {

            return gisTownSuburb
                    .Select(a => new DropDownListItems
                    {
                        Value = a.Id,
                        Text = a.Name,
                    })
                    .Distinct()
                    .OrderBy(a => a.Text)
                    .ToList();

        }
    }
}
