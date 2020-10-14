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
    public static class EventTransform
    {

        /// <summary>
        /// Convert Event Object into Event Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Event</param>
        ///<returns>IEnumerable Event</returns>
        ///
        public static IEnumerable<EventViewModel> ToListViewModel(
            this IQueryable<DataAccess.Event> entity)
        {
            return entity
                .Include(c => c.EventType)
                .Include(c => c.Frequency)
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new EventViewModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            EventTypeId = a.EventTypeId,
                            RequiresRsvp = a.RequiresRsvp,
                            RequiresSubscription = a.RequiresSubscription,
                            FrequencyId = a.FrequencyId,
                            Frequency = a.Frequency.Name,
                            EventType = a.EventType.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Event Object into Event Entity
        /// </summary>
        ///<param name="model">Event</param>
        ///<param name="EventEntity">DataAccess.Event</param>
        ///<returns>DataAccess.Event</returns>
        public static DataAccess.Event ToEntity(this EventViewModel model,
             DataAccess.Event entity)
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
            entity.EventTypeId = model.EventTypeId;
            entity.RequiresRsvp = model.RequiresRsvp;
            entity.RequiresSubscription = model.RequiresSubscription;
            entity.FrequencyId = model.FrequencyId;
           
            return entity;
        }

        /// <summary>
        /// Convert Event Entity  into Event Object
        /// </summary>
        ///<param name="model">EventViewModel</param>
        ///<param name="EventEntity">DataAccess.Event</param>
        ///<returns>EventViewModel</returns>
        public static EventViewModel ToViewModel(
         this DataAccess.Event entity,
         EventViewModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.RequiresRsvp = entity.RequiresRsvp;
            model.RequiresSubscription = entity.RequiresSubscription;
            model.Frequency = entity.Frequency.Name;
            model.Calendars = entity.Calendars.AsQueryable().ToQueryListViewModel();
            model.FrequencyId = entity.FrequencyId;
            model.EventTypeId = entity.EventTypeId;
            model.EventType = entity.EventType.Name;
            model.IsActive = entity.IsActive;
            return model;
        }

    }

}
