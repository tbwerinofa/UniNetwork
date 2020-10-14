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
    public static class RaceTypeTransform
    {

        /// <summary>
        /// Convert RaceType Object into RaceType Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.RaceType</param>
        ///<returns>IEnumerable RaceType</returns>
        ///
        public static IEnumerable<RaceTypeViewModel> ToListViewModel(
            this IQueryable<DataAccess.RaceType> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new RaceTypeViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert RaceType Object into RaceType Entity
        /// </summary>
        ///<param name="model">RaceType</param>
        ///<param name="RaceTypeEntity">DataAccess.RaceType</param>
        ///<returns>DataAccess.RaceType</returns>
        public static DataAccess.RaceType ToEntity(this RaceTypeViewModel model,
             DataAccess.RaceType entity)
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
            entity.Name = model.Name;

            return entity;
        }

        /// <summary>
        /// Convert RaceType Entity  into RaceType Object
        /// </summary>
        ///<param name="model">RaceTypeViewModel</param>
        ///<param name="RaceTypeEntity">DataAccess.RaceType</param>
        ///<returns>RaceTypeViewModel</returns>
        public static RaceTypeViewModel ToViewModel(
         this DataAccess.RaceType entity,
         RaceTypeViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
