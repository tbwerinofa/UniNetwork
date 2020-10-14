using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class GlobalGlobalRegionTransform
    {
        /// <summary>
        /// Convert GlobalRegion Object into GlobalRegion Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.GlobalRegion</param>
        ///<returns>IEnumerable GlobalRegion</returns>
        ///
        public static IEnumerable<GlobalRegionViewModel> ToListViewModel(
            this IQueryable<DataAccess.GlobalRegion> entity)
        {
            return entity
               .Include(c => c.UpdatedUser)
                       .Include(c => c.CreatedUser)
                    .Select(a =>
                        new GlobalRegionViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsActive = a.IsActive,
                            SessionUserId = a.CreatedUserId,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert GlobalRegion Object into GlobalRegion Entity
        /// </summary>
        ///<param name="model">GlobalRegion</param>
        ///<param name="GlobalRegionEntity">DataAccess.GlobalRegion</param>
        ///<returns>DataAccess.GlobalRegion</returns>
        public static GlobalRegion ToEntity(this GlobalRegionViewModel model,
            GlobalRegion entity)
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
        /// Convert GlobalRegion Entity  into GlobalRegion Object
        /// </summary>
        ///<param name="model">GlobalRegionViewModel</param>
        ///<param name="GlobalRegionEntity">DataAccess.GlobalRegion</param>
        ///<returns>GlobalRegionViewModel</returns>
        public static GlobalRegionViewModel ToViewModel(
         this DataAccess.GlobalRegion entity,
         GlobalRegionViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            return model;
        }



    }
}
