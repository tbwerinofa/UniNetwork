using BusinessObject;
using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class WinnerTransform
    {
        /// <summary>
        /// Convert MNC Object into MNC Entity
        /// </summary>
        ///<param name="model">MNC</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>DataAccess.MNC</returns>
        public static Winner ToEntity(this WinnerViewModel model,Winner entity
           )
        {
            if (entity.Id == 0)
            {
                entity.CreatedUserId = model.SessionUserId;
            }
            else
            {
                entity.IsActive = model.IsActive;
                entity.UpdatedUserId = model.SessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            entity.FinYearId = model.FinYearId;
            entity.CalendarMonthId = model.CalendarMonthId;
            entity.MemberId = model.MemberId;
            entity.AwardId = model.AwardId;
            return entity;
        }

        public static IEnumerable<WinnerViewModel> ToListViewModel(
           this IQueryable<Winner> entity)
        {
            return entity
                .Include(c => c.FinYear)
                .Include(c => c.Member.Person)
                .Include(c => c.CalendarMonth)
                .Include(c => c.Award.Gender)
                 .Include(c => c.Award.Frequency)
               .Include(c => c.UpdatedUser)
                   .Include(c => c.CreatedUser)
                 .IgnoreQueryFilters()
                .AsNoTracking()
                .ToList().Select(a =>
                        new WinnerViewModel
                        {
                            IsActive = a.IsActive,
                            Id = a.Id,
                            FinYear = a.FinYear.Name,
                            Frequency = a.Award.Frequency.Name,
                            CalendarMonth = a.CalendarMonth != null? a.CalendarMonth.Name: string.Empty,
                            Member = a.Member.Person.FullName,
                            Award = a.Award.Name,
                            Ordinal = a.Award.Ordinal,
                            Gender = a.Award.Gender != null ? a.Award.Gender.Name : string.Empty,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }

        /// <summary>
        /// Convert MNC Entity  into MNC Object
        /// </summary>
        ///<param name="model">WinnerViewModel</param>
        ///<param name="MNCEntity">DataAccess.MNC</param>
        ///<returns>WinnerViewModel</returns>
        public static WinnerViewModel ToViewModel(this Winner entity,
            WinnerViewModel model)
        {
            model.Id = entity.Id;
            model.IsActive = entity.IsActive;
            model.FinYearId = entity.FinYearId;
            model.CalendarMonthId = entity.CalendarMonthId;
            model.MemberId = entity.MemberId;
            model.AwardId = entity.AwardId;
            model.FrequencyId = entity.Award.Frequency.Id;
            model.FinYear = entity.FinYear.Name;
            model.CalendarMonth = entity.CalendarMonth != null ? entity.CalendarMonth.Name : string.Empty;
            model.Member = entity.Member.Person.FullName;
            model.Award = entity.Award.Name;

            return model;
        }


    }
}
