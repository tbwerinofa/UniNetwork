using BusinessObject.ViewModel;
using DataAccess;
using DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Transform
{
    public static class TimeTrialDistanceTransform
    {

        public static IEnumerable<TimeTrialDistanceViewModel> ToListViewModel(
         this IQueryable<DataAccess.TimeTrialDistance> entity)
        {
            return entity
                .Include(c => c.Distance)
                 .Include(c => c.TimeTrial)
                .AsNoTracking()
                .Select(a =>
                        new TimeTrialDistanceViewModel
                        {
                            Id = a.Id,
                            DistanceId = a.DistanceId,
                            TimeTrialId = a.TimeTrialId,
                            IsActive = a.IsActive,
                            Distance = a.Distance.Name,
                        });
        }
        
        public static TimeTrialDistance ToEntity(this TimeTrialDistance entity, int referralId, int containerId, string sessionUserId)
        {

            if (entity.Id > 0)
            {
                entity.UpdatedUserId = sessionUserId;
                entity.UpdatedTimestamp = DateTime.Now;
            }
            else
            {
                entity.TimeTrialId = containerId;
                entity.DistanceId = referralId;
                entity.CreatedUserId = sessionUserId;

            }



            return entity;
        }

        public static IEnumerable<TimeTrialDistanceViewModel> ToQueryListViewModel(
        this IQueryable<DataAccess.TimeTrialDistance> entity)
        {
            return entity
                .Include(c => c.Distance)
                 .Include(c => c.TimeTrial.Calendar.Event)
                 .Include(c => c.TimeTrialResults).ThenInclude(c => c.Member.Person.Gender)
                 .Include(c => c.TimeTrialResults).ThenInclude(c => c.TimeTrialDistance.Distance)

                .AsNoTracking()
                .Select(a =>
                        new TimeTrialDistanceViewModel
                        {
                            Id = a.Id,
                            DistanceId = a.DistanceId,
                            TimeTrialId = a.TimeTrialId,
                            IsActive = a.IsActive,
                            Distance = a.Distance.Name,
                            Participant = a.TimeTrialResults.Count(),
                            TimeTrialResults = a.TimeTrialResults.AsQueryable().ToQueryListViewModel()
                        });
        }
    }
}
