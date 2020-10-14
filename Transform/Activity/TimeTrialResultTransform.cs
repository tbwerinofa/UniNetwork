using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class TimeTrialResultTransform
    {

        public static IEnumerable<TimeTrialResultViewModel> ToListViewModel(
         this IQueryable<DataAccess.TimeTrialResult> entity)
        {
            return entity
                .Include(c => c.Member.Person.AgeGroup)
                .Include(c => c.Member.Person.Gender)
                 .Include(c => c.TimeTrialDistance.Distance)
                 .Include(c => c.TimeTrialDistance.TimeTrial.Calendar)
                  .Include(c => c.CreatedUser)
                   .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new TimeTrialResultViewModel
                        {
                            Id = a.Id,
                            MemberNo = a.Member.MemberNo,
                            FullName = a.Member.Person.FullName,
                            Gender = a.Member.Person.Gender.Name,
                            Distance = a.TimeTrialDistance.Distance.Name,
                            Measurement = a.TimeTrialDistance.Distance.Measurement,
                            EventDate = a.TimeTrialDistance.TimeTrial.Calendar.ScheduleDate,
                            MemberId = a.MemberId,
                            Position = a.Position,
                            AgeGroup =a.Member.Person.AgeGroup.Name,
                            TimeTaken = a.TimeTaken??TimeSpan.MinValue,
                            AveragePace = a.AveragePace ?? TimeSpan.MinValue,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }


        public static TimeTrialResult ToEntity(this TimeTrialResult entity, int referralId, int containerId,TimeSpan timeTaken,int? position,int ageGroupId,string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.TimeTrialDistanceId = containerId;
                entity.MemberId = referralId;
                entity.Position = position;
                entity.TimeTaken = timeTaken;
                entity.CreatedUserId = sessionUserID;
            }
            return entity;
        }

        /// <summary>
        /// Convert TimeTrialResult Object into TimeTrialResult Entity
        /// </summary>
        ///<param name="model">TimeTrialResult</param>
        ///<param name="TimeTrialResultEntity">DataAccess.TimeTrialResult</param>
        ///<returns>DataAccess.TimeTrialResult</returns>
        public static DataAccess.TimeTrialResult ToEntity(this TimeTrialResultViewModel model,
             DataAccess.TimeTrialResult entity)
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
            entity.TimeTrialDistanceId = model.TimeTrialDistanceId;
            entity.TimeTaken = model.TimeTaken;
            entity.MemberId = model.MemberId;
            entity.Position = model.Position;
            return entity;
        }

        /// <summary>
        /// Convert TimeTrialResult Entity  into TimeTrialResult Object
        /// </summary>
        ///<param name="model">TimeTrialResultViewModel</param>
        ///<param name="TimeTrialResultEntity">DataAccess.TimeTrialResult</param>
        ///<returns>TimeTrialResultViewModel</returns>
        public static TimeTrialResultViewModel ToViewModel(
         this DataAccess.TimeTrialResult entity,
         TimeTrialResultViewModel model)
        {
            model.Id = entity.Id;
            model.TimeTrialDistanceId = entity.TimeTrialDistanceId;
            model.TimeTaken = entity.TimeTaken??TimeSpan.MinValue;
            model.AveragePace = entity.AveragePace ?? TimeSpan.MinValue;
            model.MemberId = entity.MemberId;
            model.IsActive = entity.IsActive;
            return model;
        }

        public static IEnumerable<TimeTrialResultViewModel> ToQueryListViewModel(
   this IQueryable<DataAccess.TimeTrialResult> entity)
        {
            return entity
                .Include(c => c.Member.Person.Gender)
                 .Include(c => c.TimeTrialDistance.Distance) 
                 .Include(c => c.TimeTrialDistance.TimeTrial.Calendar.FinYear)
                .AsNoTracking()
                .Select(a =>
                        new TimeTrialResultViewModel
                        {
                            Id = a.Id,
                            TimeTrialId = a.TimeTrialDistance.TimeTrialId,
                            TimeTrialDistanceId = a.TimeTrialDistanceId,
                            Position = a.Position,
                            MemberNo = a.Member.MemberNo,
                            FullName = a.Member.Person.FullName,
                            Gender = a.Member.Person.Gender.Name,
                            Distance = a.TimeTrialDistance.Distance.Name,
                            Measurement = a.TimeTrialDistance.Distance.Measurement,
                            EventDate = a.TimeTrialDistance.TimeTrial.Calendar.ScheduleDate,
                            MemberId = a.MemberId,
                            PersonId = a.Member.PersonId,
                            Year = a.TimeTrialDistance.TimeTrial.Calendar.FinYear.Name,
                            TimeTaken = a.TimeTaken ?? TimeSpan.MinValue,
                            AveragePace = a.AveragePace ?? TimeSpan.MinValue,
                            IsActive = a.IsActive

                        });
        }

    }
}
