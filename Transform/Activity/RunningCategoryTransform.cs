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
    public static class RunningCategoryTransform
    {

        /// <summary>
        /// Convert RunningCategory Object into RunningCategory Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.RunningCategory</param>
        ///<returns>IEnumerable RunningCategory</returns>
        ///
        public static IEnumerable<RunningCategoryViewModel> ToListViewModel(
            this IQueryable<DataAccess.RunningCategory> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new RunningCategoryViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert RunningCategory Object into RunningCategory Entity
        /// </summary>
        ///<param name="model">RunningCategory</param>
        ///<param name="RunningCategoryEntity">DataAccess.RunningCategory</param>
        ///<returns>DataAccess.RunningCategory</returns>
        public static DataAccess.RunningCategory ToEntity(this RunningCategoryViewModel model,
             DataAccess.RunningCategory entity)
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
        /// Convert RunningCategory Entity  into RunningCategory Object
        /// </summary>
        ///<param name="model">RunningCategoryViewModel</param>
        ///<param name="RunningCategoryEntity">DataAccess.RunningCategory</param>
        ///<returns>RunningCategoryViewModel</returns>
        public static RunningCategoryViewModel ToViewModel(
         this DataAccess.RunningCategory entity,
         RunningCategoryViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
