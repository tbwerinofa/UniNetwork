using BusinessObject;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class EventTypeTransform
    {

        /// <summary>
        /// Convert EventType Object into EventType Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.EventType</param>
        ///<returns>IEnumerable EventType</returns>
        ///
        public static IEnumerable<EventTypeViewModel> ToListViewModel(
            this IQueryable<DataAccess.EventType> entity)
        {
            return entity
                // .Include(c => c.CreatedUser)
                //.Include(c => c.UpdatedUser)

                .AsNoTracking()
                .Select(a =>
                        new EventTypeViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName,
                            IsActive = a.IsActive
                        });
        }

        /// <summary>
        /// Convert EventType Object into EventType Entity
        /// </summary>
        ///<param name="model">EventType</param>
        ///<param name="EventTypeEntity">DataAccess.EventType</param>
        ///<returns>DataAccess.EventType</returns>
        public static DataAccess.EventType ToEntity(this EventTypeViewModel model,
             DataAccess.EventType entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.Discriminator = model.Discriminator;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.Name = model.Name;
            entity.IsActive = model.IsActive;
            return entity;
        }

        /// <summary>
        /// Convert EventType Entity  into EventType Object
        /// </summary>
        ///<param name="model">EventTypeViewModel</param>
        ///<param name="EventTypeEntity">DataAccess.EventType</param>
        ///<returns>EventTypeViewModel</returns>
        public static EventTypeViewModel ToViewModel(
         this DataAccess.EventType entity,
         EventTypeViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Discriminator = entity.Discriminator;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
