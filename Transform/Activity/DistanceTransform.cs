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
    public static class DistanceTransform
    {

        /// <summary>
        /// Convert Distance Object into Distance Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Distance</param>
        ///<returns>IEnumerable Distance</returns>
        ///
        public static IEnumerable<DistanceViewModel> ToListViewModel(
            this IQueryable<DataAccess.Distance> entity)
        {
            return entity
                  .Include(c => c.MeasurementUnit)
                    .Include(c => c.CreatedUser)
               .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new DistanceViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Measurement = a.Measurement,
                            MeasurementUnitId = a.MeasurementUnitId,
                            Discriminator = a.Discriminator,
                            MeasurementUnit = a.MeasurementUnit.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Distance Object into Distance Entity
        /// </summary>
        ///<param name="model">Distance</param>
        ///<param name="DistanceEntity">DataAccess.Distance</param>
        ///<returns>DataAccess.Distance</returns>
        public static DataAccess.Distance ToEntity(this DistanceViewModel model,
             DataAccess.Distance entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.IsActive = model.IsActive;
                entity.Discriminator = model.Discriminator;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.Name = model.Name;
            entity.MeasurementUnitId = model.MeasurementUnitId;
            entity.Measurement = model.Measurement;

            return entity;
        }

        /// <summary>
        /// Convert Distance Entity  into Distance Object
        /// </summary>
        ///<param name="model">DistanceViewModel</param>
        ///<param name="DistanceEntity">DataAccess.Distance</param>
        ///<returns>DistanceViewModel</returns>
        public static DistanceViewModel ToViewModel(
         this DataAccess.Distance entity,
         DistanceViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.IsActive = entity.IsActive;
            model.MeasurementUnitId = entity.MeasurementUnitId;
            model.Measurement = entity.Measurement;
            model.Discriminator = entity.Discriminator;
            return model;
        }

    }

}
