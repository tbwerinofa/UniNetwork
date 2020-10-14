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
    public static class CalendarTransform
    {

        /// <summary>
        /// Convert Calendar Object into Calendar Entity
        /// </summary>
        ///<param name="entity">IQueryable DataAccess.Calendar</param>
        ///<returns>IEnumerable Calendar</returns>
        ///
        public static IEnumerable<CalendarViewModel> ToListViewModel(
            this IQueryable<DataAccess.Calendar> entity)
        {
            return entity
                .Include(c => c.Event)
                .Include(c => c.FinYear)
                    .Include(c => c.Venue)
                .Include(c => c.Moderators).ThenInclude(b=>b.Member.Person.FullName)
                .Include(c => c.CreatedUser)
                 .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new CalendarViewModel
                        {
                            Id = a.Id,
                            Event = a.Event.Name,
                            FinYear = a.FinYear.Name,
                            Venue = a.Venue.Name,
                            ScheduleDateString = a.ScheduleDate.ToCustomLongDate(),
                            RevisedDateString = a.RevisedDate.ToCustomLongDate(),
                            ScheduleDate = a.ScheduleDate,
                            RevisedDate = a.RevisedDate,
                            StartTime = a.StartTime,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName,
                            Members = a.Moderators.Select(b=>b.Member.Person.FullName)
                        });
        }

        /// <summary>
        /// Convert Calendar Object into Calendar Entity
        /// </summary>
        ///<param name="model">Calendar</param>
        ///<param name="CalendarEntity">DataAccess.Calendar</param>
        ///<returns>DataAccess.Calendar</returns>
        public static DataAccess.Calendar ToEntity(this CalendarViewModel model,
             DataAccess.Calendar entity)
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
            entity.EventId = model.EventId;
            entity.FinYearId = model.FinYearId;
            entity.StartTime = model.StartTime;
            entity.VenueId = model.VenueId;
            entity.ScheduleDate = model.ScheduleDate;
            entity.RevisedDate = model.RevisedDate;
           
            return entity;
        }

        /// <summary>
        /// Convert Calendar Entity  into Calendar Object
        /// </summary>
        ///<param name="model">CalendarViewModel</param>
        ///<param name="CalendarEntity">DataAccess.Calendar</param>
        ///<returns>CalendarViewModel</returns>
        public static CalendarViewModel ToViewModel(
         this DataAccess.Calendar entity,
         CalendarViewModel model)
        {
            model.Id = entity.Id;
            model.Event = entity.Event.Name;
            model.FinYear = entity.FinYear.Name;
            model.Venue = entity.Venue.Name;
            model.EventId = entity.EventId;
            model.FinYearId = entity.FinYearId;
            model.VenueId = entity.VenueId;
            model.Notes = entity.Notes;
            model.IsActive = entity.IsActive;
            model.ScheduleDate = entity.ScheduleDate;
            model.RevisedDate = entity.RevisedDate;
            model.StartTime = entity.StartTime;
            model.ScheduleDateString = entity.RevisedDate.HasValue ? entity.RevisedDate.ToCustomLongDate(): entity.ScheduleDate.ToCustomLongDate();
            model.ModeratorIds = entity.Moderators.Select(a => a.MemberId);
            return model;
        }

        public static DashboardItem ToDashBoardItem(
     this DataAccess.Calendar entity)
        {

            if (entity != null)
            {
                return new DashboardItem
                {
                    Url = "/Calendar/Detail/" + entity.Id,
                    Icon = "fa-newspaper-o",
                    Name = "Event: " + entity.Event.Name,
                    Description = "Venue:" + entity.Venue.Name,
                    Message = entity.ScheduleDate.ToCustomLongDate(),
                    TimeString = entity.StartTime.ToString(),
                    DateTimeStamp = entity.CreatedTimestamp
                };
            }

            return null;
        }

        public static IEnumerable<CalendarViewModel> ToQueryListViewModel(
    this IQueryable<DataAccess.Calendar> entity)
        {
            return entity
                .AsNoTracking()
                .Select(a =>
                        new CalendarViewModel
                        {
                            Id = a.Id,
                            Event = a.Event.Name,
                            FinYear = a.FinYear.Name,
                            Venue = a.Venue.Name,
                            ScheduleDateString = a.ScheduleDate.ToCustomLongDate(),
                            RevisedDateString = a.RevisedDate.ToCustomLongDate(),
                            ScheduleDate = a.ScheduleDate,
                            RevisedDate = a.RevisedDate,
                            StartTime = a.StartTime,
                            IsActive = a.IsActive,
                            Members = a.Moderators.Select(b => b.Member.Person.FullName)
                        });
        }
    }

}
