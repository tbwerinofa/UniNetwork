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
    public static class VenueTransform
    {

        /// <summary>
        /// Convert Venue Object into Venue Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Venue</param>
        ///<returns>IEnumerable Venue</returns>
        ///
        public static IEnumerable<VenueViewModel> ToListViewModel(
            this IQueryable<Venue> entity)
        {
            return entity
               .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Select(a =>
                        new VenueViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                             Latitude = a.Latitude,
                            Longitude = a.Longitude,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName,

                        });
        }

        /// <summary>
        /// Convert Venue Object into Venue Entity
        /// </summary>
        ///<param name="model">Venue</param>
        ///<param name="VenueEntity">DataAccess.Venue</param>
        ///<returns>DataAccess.Venue</returns>
        public static DataAccess.Venue ToEntity(this VenueViewModel model,
             DataAccess.Venue entity)
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
            entity.Name = model.Name;
            entity.Longitude = model.Longitude;
            entity.Latitude = model.Latitude;
            entity.IsActive = model.IsActive;
            return entity;
        }

        /// <summary>
        /// Convert Venue Entity  into Venue Object
        /// </summary>
        ///<param name="model">VenueViewModel</param>
        ///<param name="VenueEntity">DataAccess.Venue</param>
        ///<returns>VenueViewModel</returns>
        public static VenueViewModel ToViewModel(
         this DataAccess.Venue entity,
         VenueViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Latitude = entity.Latitude;
            model.Longitude = entity.Longitude;
            model.IsActive = entity.IsActive;
            return model;
        }


    }

}
