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
    public static class TimeTrialTransform
    {

        /// <summary>
        /// Convert Race Object into Race Entity
        /// </summary>
        ///<param name="entity">IQueryable TimeTrial</param>
        ///<returns>IEnumerable Race</returns>
        ///
        public static IEnumerable<TimeTrialViewModel> ToListViewModel(
            this IQueryable<TimeTrial> entity)
        {
            return entity
                .Include(c => c.Calendar.FinYear)
                .Include(c => c.Calendar.Venue)
                .Include(c => c.RaceType)
                 .Include(c => c.TimeTrialDistances).ThenInclude(c => c.Distance)
                  .Include(c => c.TimeTrialDistances).ThenInclude(c => c.TimeTrial)
                .Include(c => c.CreatedUser)
                .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new TimeTrialViewModel
                        {
                            Id = a.Id,
                            FinYear = a.Calendar.FinYear.Name,
                            ScheduleDateString = a.Calendar.ScheduleDate.ToCustomLongDate(),
                            TimeTrialDistances = a.TimeTrialDistances.AsQueryable().ToListViewModel(),
                            StartTime = a.Calendar.StartTime,
                            Venue = a.Calendar.Venue.Name,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName
                        });
        }

        /// <summary>
        /// Convert Race Object into Race Entity
        /// </summary>
        ///<param name="model">Race</param>
        ///<param name="RaceEntity">TimeTrial</param>
        ///<returns>TimeTrial</returns>
        public static TimeTrial ToEntity(this TimeTrialViewModel model,
             TimeTrial entity)
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
                entity.IsActive = model.IsActive;
                entity.CalendarId = model.CalendarId;
                entity.RaceTypeId = model.RaceTypeId;
            }
            else
            {
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
       

            return entity;
        }

        /// <summary>
        /// Convert Race Entity  into Race Object
        /// </summary>
        ///<param name="model">TimeTrialViewModel</param>
        ///<param name="RaceEntity">TimeTrial</param>
        ///<returns>TimeTrialViewModel</returns>
        public static TimeTrialViewModel ToViewModel(
         this TimeTrial entity,
         TimeTrialViewModel model)
        {
            model.Id = entity.Id;
            model.CalendarId = entity.CalendarId;
            model.RaceTypeId = entity.RaceTypeId;
            model.FinYear = entity.Calendar.FinYear.Name;
            model.Calendar = entity.Calendar.ToViewModel(new CalendarViewModel());
            model.DistanceIds = entity.TimeTrialDistances.Select(a => a.DistanceId);
            return model;
        }

        //public static RaceQLViewModel ToQueryViewModel(
        // this TimeTrial entity,
        // RaceQLViewModel model)
        //{
        //    model.Id = entity.Id;
        //    model.Theme = entity.Theme;
        //    model.RaceDefinition = entity.RaceDefinition.Name;
        //    model.FinYearId = entity.FinYearId;
        //    model.FinYear = entity.FinYear.Name;
        //    model.Province = entity.RaceDefinition.Province.Name;
        //    model.Country = entity.RaceDefinition.Province.Country.Name;
        //    model.ProvinceId = entity.RaceDefinition.ProvinceId;
        //    model.CountryId = entity.RaceDefinition.Province.CountryId;
        //    model.RaceDefinitionId = entity.RaceDefinitionId;
        //    model.TimeTrialDistances = entity.TimeTrialDistances.AsQueryable().ToQueryListViewModel();
        //     model.Participants = entity.TimeTrialDistances.Sum(b => b.RaceResults.Count());
        //    return model;
        //}

        //public static IEnumerable<RaceQLViewModel> ToQueryListViewModel(
        //   this IQueryable<TimeTrial> entity)
        //{
        //    return entity
        //        .Include(c => c.FinYear)
        //         .Include(c => c.RaceDefinition.Province.Country)
        //        .Include(c => c.RaceDefinition.RaceType)
        //         .Include(c => c.TimeTrialDistances).ThenInclude(c => c.Distance)
        //          .Include(c => c.TimeTrialDistances).ThenInclude(c => c.Race)
        //         .Include(c => c.TimeTrialDistances).ThenInclude(c => c.RaceResults)
        //        .AsNoTracking()
        //        .Where(a=>a.TimeTrialDistances.Any(b=>b.RaceResults.Any()))
        //        .Select(a =>
        //                new RaceQLViewModel
        //                {
        //                    Id = a.Id,
        //                    FinYear = a.FinYear.Name,
        //                    RaceDefinition = a.RaceDefinition.Name,
        //                    Theme = a.Theme,
        //                    Province = a.RaceDefinition.Province.Name,
        //                    Country = a.RaceDefinition.Province.Country.Name,
        //                    TimeTrialDistances = a.TimeTrialDistances.AsQueryable().ToListViewModel(),
        //                    Participants = a.TimeTrialDistances.Sum(b => b.RaceResults.Count()),
        //                    IsActive = a.IsActive,
        //                });
        //}
    }

}
