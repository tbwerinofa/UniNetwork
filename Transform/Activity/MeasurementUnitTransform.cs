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
    public static class MeasurementUnitTransform
    {

        /// <summary>
        /// Convert MeasurementUnit Object into MeasurementUnit Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.MeasurementUnit</param>
        ///<returns>IEnumerable MeasurementUnit</returns>
        ///
        public static IEnumerable<MeasurementUnitViewModel> ToListViewModel(
            this IQueryable<DataAccess.MeasurementUnit> entity)
        {
            return entity
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new MeasurementUnitViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert MeasurementUnit Object into MeasurementUnit Entity
        /// </summary>
        ///<param name="model">MeasurementUnit</param>
        ///<param name="MeasurementUnitEntity">DataAccess.MeasurementUnit</param>
        ///<returns>DataAccess.MeasurementUnit</returns>
        public static DataAccess.MeasurementUnit ToEntity(this MeasurementUnitViewModel model,
             DataAccess.MeasurementUnit entity)
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
        /// Convert MeasurementUnit Entity  into MeasurementUnit Object
        /// </summary>
        ///<param name="model">MeasurementUnitViewModel</param>
        ///<param name="MeasurementUnitEntity">DataAccess.MeasurementUnit</param>
        ///<returns>MeasurementUnitViewModel</returns>
        public static MeasurementUnitViewModel ToViewModel(
         this DataAccess.MeasurementUnit entity,
         MeasurementUnitViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
