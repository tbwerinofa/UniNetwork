using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class RaceResultTransform
    {

        public static IEnumerable<RaceResultViewModel> ToListViewModel(
         this IQueryable<DataAccess.RaceResult> entity)
        {
            return entity
                .Include(c => c.Member.Person)
                 .Include(c => c.RaceDistance.Distance)
                  .Include(c => c.CreatedUser)
                   .Include(c => c.UpdatedUser)
                .AsNoTracking()
                .Select(a =>
                        new RaceResultViewModel
                        {
                            Id = a.Id,
                            RaceDistanceId = a.RaceDistanceId,
                            MemberNo = a.Member.MemberNo,
                            FullName = a.Member.Person.FullName,
                            Gender = a.Member.Person.Gender.Name,
                            Distance = a.RaceDistance.Distance.Name,
                            Measurement = a.RaceDistance.Distance.Measurement,
                            EventDate = a.RaceDistance.EventDate,
                            MemberId = a.MemberId,
                            PersonId = a.Member.PersonId,
                            Position = a.Position,
                            AgeGroup =a.AgeGroup.Name,
                            TimeTaken = a.TimeTaken??TimeSpan.MinValue,
                            AveragePace = a.AveragePace ?? TimeSpan.MinValue,
                            IsActive = a.IsActive,
                            LastUpdate = a.UpdatedTimestamp.HasValue ? a.UpdatedTimestamp.ToCustomLongDate() : a.CreatedTimestamp.ToCustomLongDate(),
                            LastUpdatedUser = a.UpdatedUserId != null ? a.UpdatedUser.FullName : a.CreatedUser.FullName

                        });
        }


        public static RaceResult ToEntity(this RaceResult entity, int referralId, int containerId,TimeSpan timeTaken,int? position,int ageGroupId,string sessionUserID)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserID;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.RaceDistanceId = containerId;
                entity.AgeGroupId = ageGroupId;
                entity.MemberId = referralId;
                entity.Position = position;
                entity.TimeTaken = timeTaken;
                entity.CreatedUserId = sessionUserID;
            }
            return entity;
        }

        /// <summary>
        /// Convert RaceResult Object into RaceResult Entity
        /// </summary>
        ///<param name="model">RaceResult</param>
        ///<param name="RaceResultEntity">DataAccess.RaceResult</param>
        ///<returns>DataAccess.RaceResult</returns>
        public static DataAccess.RaceResult ToEntity(this RaceResultViewModel model,
             DataAccess.RaceResult entity)
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
            entity.RaceDistanceId = model.RaceDistanceId;
            entity.TimeTaken = model.TimeTaken;
            entity.MemberId = model.MemberId;
            entity.Position = model.Position;
            return entity;
        }

        /// <summary>
        /// Convert RaceResult Entity  into RaceResult Object
        /// </summary>
        ///<param name="model">RaceResultViewModel</param>
        ///<param name="RaceResultEntity">DataAccess.RaceResult</param>
        ///<returns>RaceResultViewModel</returns>
        public static RaceResultViewModel ToViewModel(
         this DataAccess.RaceResult entity,
         RaceResultViewModel model)
        {
            model.Id = entity.Id;
            model.RaceDistanceId = entity.RaceDistanceId;
            model.TimeTaken = entity.TimeTaken??TimeSpan.MinValue;
            model.AveragePace = entity.AveragePace ?? TimeSpan.MinValue;
            model.MemberId = entity.MemberId;
            model.IsActive = entity.IsActive;
            return model;
        }

        public static IEnumerable<RaceResultViewModel> ToQueryListViewModel(
   this IQueryable<DataAccess.RaceResult> entity)
        {
            return entity
                .Include(c => c.Member.Person.Gender)
                .Include(c => c.Member.Person.AgeGroup)
                 .Include(c => c.RaceDistance.Distance)
                  .Include(c => c.RaceDistance.Race.FinYear)
                .AsNoTracking()
                .Select(a =>
                        new RaceResultViewModel
                        {
                            Id = a.Id,
                            RaceDistanceId = a.RaceDistanceId,
                            Position = a.Position,
                            MemberNo = a.Member.MemberNo,
                            FullName = a.Member.Person.FullName,
                            Gender = a.Member.Person.Gender.Name,
                            AgeGroup = a.Member.Person.AgeGroup.Name,
                            RaceDefinition = a.RaceDistance.Race.RaceDefinition.Name,
                            FinYear = a.RaceDistance.Race.FinYear.Name,
                            Distance = a.RaceDistance.Distance.Name,
                            Measurement = a.RaceDistance.Distance.Measurement,
                            EventDate = a.RaceDistance.EventDate,
                            MemberId = a.MemberId,
                            PersonId = a.Member.PersonId,
                            TimeTaken = a.TimeTaken ?? TimeSpan.MinValue,
                            AveragePace = a.AveragePace ?? TimeSpan.MinValue,
                            IsActive = a.IsActive,
                        });
        }

    }
}
